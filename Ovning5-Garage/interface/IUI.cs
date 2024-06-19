// Anv�ndargr�nssnitt (UI) f�r garagehanteringssystemet.
interface IUI
{
    double PromptForDouble(string query = "", string successFeedback = "");
    FuelType PromptForFuelType();
    uint PromptForUint(string query = "", string successFeedback = "");
    Tuple<string, string, uint, string> AskForVehicleDetails();

    // Visa huvudmenyn f�r anv�ndaren och returnera vald �tg�rd.
    UserAction ShowMainMenu(bool garageAlreadyExists = true);

    string PromptForRegNr();
    string PromptForVehicleType(bool permitAny = false);
    string PromptForColor();

    uint? PromptWheelCountToSearchFor();
}
