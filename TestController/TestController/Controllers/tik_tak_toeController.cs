using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace TestController.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GameBoard _gameBoard;

        private readonly ILogger<GamesController> _logger;
		private readonly IHubContext<MessageHub> _hub;

		public GamesController(ILogger<GamesController> logger, IHubContext<MessageHub> hubContext)
        {
            _logger = logger;
            _hub = hubContext;
			_gameBoard = new GameBoard();
		}

		public class MoveData
		{
			public string User1 { get; set; }

			public string User2 { get; set; }
			public int row { get; set; }
			public int column { get; set; }

			public string[] Board_Row1 {get; set;}

			public string[] Board_Row2 { get; set; }

			public string[] Board_Row3 { get; set; }

			public int priority { get; set; }

			public string message {  get; set; }
		}


		[HttpPost ("MoveAnalysis")]
        public IActionResult MakeMove([FromBody] MoveData moveData)
        {
			int row = moveData.row;
			int col = moveData.column;
			string Username1 = moveData.User1;
			string Username2 = moveData.User2;
			string[] Board_Row1 = moveData.Board_Row1;
			string[] Board_Row2 = moveData.Board_Row2;
			string[] Board_Row3 = moveData.Board_Row3;
			int Priority = moveData.priority;
			if (Username1 == "" || Username2 == "")
			{
				_logger.LogWarning("Bad request, users are not initialized!");
				return BadRequest("Users are not initialized!");
			}
			if (_gameBoard.HaveError(Board_Row1, Board_Row2, Board_Row3))
			{
				_hub.Clients.All.SendAsync("Draw. The board is complete");
				moveData.message = "Ничья";
				return Ok(moveData);
			}
			if (row<1 || col<1|| row>3 || col > 3)
			{
				_logger.LogWarning("Bad request, The value of a column or row must be between 1 and 3");
				return BadRequest(col);
			}
			switch (row)
			{
				case 1:
					if (Board_Row1[col - 1] == "")
					{
						if (Priority == 0)
						{
							Board_Row1[col - 1] = "X";

							break;
						}
						else
						{
							Board_Row1[col - 1] = "O";
							break;
						}
					}
					else
					{
						_logger.LogWarning("The cell is occupied");
						return BadRequest("The cell is occupied");
					}

				case 2:
					if (Board_Row2[col - 1] == "")
					{
						if (Priority == 0)
						{
							Board_Row2[col - 1] = "X";
							break;
						}
						else
						{
							Board_Row2[col - 1] = "O";
							break;
						}
					}
					else
					{
						_logger.LogWarning("The cell is occupied");
						return BadRequest("The cell is occupied");
					}

				case 3:
					if (Board_Row3[col - 1] == "")
					{
						if (Priority == 0)
						{
							Board_Row3[col - 1] = "X";
							break;
						}
						else
						{
							Board_Row3[col - 1] = "O";
							break;
						}
					}
					else
					{
						_logger.LogWarning("The cell is occupied");
						return BadRequest("The cell is occupied");
					}
			}
			moveData.Board_Row1 = Board_Row1;
			moveData.Board_Row2 = Board_Row2;
			moveData.Board_Row3 = Board_Row3;
			moveData.priority = Math.Abs(Priority - 1);
			if (_gameBoard.HaveWinner(Board_Row1, Board_Row2, Board_Row3))
					{
						if(Priority == 0)
						{
							_hub.Clients.All.SendAsync("The second player won");
							moveData.message = "Игрок за крестики победил";

						}
						else
						{
							_hub.Clients.All.SendAsync("The first player won");
							moveData.message = "Игрок за нолики победил";
				}
					}
					return Ok(moveData);
			}

		public class StartGameData
		{
			public string User1 { get; set; }
			public string User2 { get; set; }
			public int Number1 { get; set; }
			public int Number2 { get; set; }
		}

		[HttpPost ("Start")]
        public IActionResult StartNewGame([FromBody] StartGameData data)
        {
			string User1 = data.User1;
			string User2 = data.User2;
			int Number1 = data.Number1;
			int Number2 = data.Number2;

			if (User1 == User2)
			{
				_logger.LogWarning("Bad request, the username of the first player is equal to the second");
				return BadRequest("Usernames must be different");
			}

            else
            {
                _gameBoard.Username1 = User1;
                _gameBoard.Username2 = User2;
				int Number = Random.Shared.Next(0, 55);
				if (Math.Abs(Number1 - Number) < Math.Abs(Number2 - Number))
				{
                    _gameBoard.Priority = 0;
                    _hub.Clients.All.SendAsync("The first player starts the game");
				}
				else
				{
					_gameBoard.Priority = 1;
					_hub.Clients.All.SendAsync("The second player starts the game");
				}
                return Ok(_gameBoard.Priority);
			}

		}


    }
}

/*[HttpGet ("ChooseUsername")]
	public IActionResult GetUsername(string User1, string User2)
	{
		if(User1==User2)
		{
			_logger.LogWarning("Bad request, the username of the first player is equal to the second");
			return BadRequest("Usernames must be different");
		}

		return Ok("Good usernames");
	}

/* [HttpGet]
 public IActionResult GetNumber(int  Number1, int Number2)
 {
	 int Number = Random.Shared.Next(0, 55);
	 if(Math.Abs(Number1-Number)<Math.Abs(Number2-Number)) {

		 return Ok("The first Player goes first");
	 }
	 else
	 {
		 return Ok("The second Player goes first");
	 }
 }*/

/*[HttpGet ("GiveBoard")]
        public ActionResult StartBoard()
        {
            //Возврат таблицы
            return Ok(_gameBoard.PrintBoard());
        }*/
