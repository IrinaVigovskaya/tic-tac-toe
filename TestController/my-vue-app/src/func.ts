import axios from 'axios';

export const SendUserData = async (User1: string, Number1: number, User2: string, Number2: number) => {
    if (!User1 || !User2) {
        console.log(User1, User2);
        throw new Error("Username should not be empty.");
    }
    try {
        //console.log(User1, User2);
        const response = await axios.post(
            "/api/Games/Start",
            {User1, User2, Number1, Number2}
        );
        if (response.data == "Usernames must be different")
        {
            throw new Error(response.data);
        }
        return response;
    } catch (error) {
        throw error;
    }
}


export const Move = async (row: number, column: number, User1: string, User2: string, Board_Row1: Array<string>, Board_Row2: Array<string>, Board_Row3: Array<string>, priority: number, message: string) => {
    if(!row || !column){
        throw new Error("Coordinates should not be empty.");
    }
    try{
        const response = await axios.post(
            "/api/Games/MoveAnalysis",
            {row, column, User1, User2, Board_Row1, Board_Row2, Board_Row3, priority, message}
        );

        if(response.data == "Incorrect position for the value" || response.data == "The cell is occupied"){
            throw new Error(response.data);
        }
            return response;
    } catch(error) {
        throw error;
    }
}