using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

namespace TestController
{

    public class GameBoard
    {
        public char[] Board_Row0 { get; set; }
		public char[] Board_Row1 { get; set; }
		public char[] Board_Row2 { get; set; }


		public int Priority;
          
        public Guid ID_Game { get; }

        public string Username1, Username2;

        public string PrintBoard()
        {
            string Board ="|";
            for (int i = 0; i < 3; i++)
                Board += Board_Row0[i] + "|";
            Board += "\n|";
			for (int i = 0; i < 3; i++)
				Board += Board_Row1[i] + "|";
			Board += "\n|";
			for (int i = 0; i < 3; i++)
				Board += Board_Row2[i] + "|";
            return Board;
		}

        public bool HaveWinner(string[] Board_Row0, string[] Board_Row1, string[] Board_Row2)
        {   
            if ((Board_Row0.All(x => x == Board_Row0[0]) && (Board_Row0[0]!= "")) ||
                (Board_Row1.All(x => x == Board_Row1[0]) && (Board_Row1[0] != "")) ||
                (Board_Row2.All(x => x == Board_Row2[0]) && (Board_Row2[0] != "")) ||

                ((Board_Row0[0] == Board_Row1[0] && Board_Row0[0] == Board_Row2[0])&&(Board_Row0[0] != "")) ||
                ((Board_Row0[1] == Board_Row1[1] && Board_Row0[1] == Board_Row2[1])&&(Board_Row0[1] != "")) ||
                ((Board_Row0[2] == Board_Row1[2] && Board_Row0[2] == Board_Row2[2])&&(Board_Row0[2] != "")) ||

                ((Board_Row0[0] == Board_Row2[2] && Board_Row0[0] == Board_Row1[1])&&(Board_Row0[0] != "")) ||
                ((Board_Row0[2] == Board_Row2[0] && Board_Row0[2] == Board_Row1[1]))&&(Board_Row0[2] != ""))
                return true;

                return false;
        }

        public bool HaveError(string[] Board_Row0, string[] Board_Row1, string[] Board_Row2)
        {
            if ((Board_Row0[0] != "" && Board_Row0[1] != "" && Board_Row0[2] != "") &&
                (Board_Row1[0] != "" && Board_Row1[1] != "" && Board_Row1[2] != "") &&
                (Board_Row2[0] != "" && Board_Row2[1] != "" && Board_Row2[2] != ""))
                return true;
            return false;
        }

        public GameBoard()
        {
            ID_Game = Guid.NewGuid();
            Board_Row0 = new char[] { ' ', ' ', ' ' };
			Board_Row1 = new char[] { ' ', ' ', ' ' };
			Board_Row2 = new char[] { ' ', ' ', ' ' };
            Username1 = null;
            Username2 = null;

		}
    }
    
    public class MessageHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }
    }
}