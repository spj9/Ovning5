// Anv�ndargr�nssnitt (UI) f�r garagehanteringssystemet.
interface IUI
{
    double AskForDouble(string query = "", string successFeedback = "");
    FuelType AskForFuelType();
    uint AskForUint(string query = "", string successFeedback = "");
    Tuple<string, string, uint, string> AskForVehicleDetails();

    // Visa huvudmenyn f�r anv�ndaren och returnera vald �tg�rd.
    UserAction ShowMainMenu(bool garageAlreadyExists = true);

    string AskForRegNr();
    string AskForVehicleType(bool permitAny = false);
    string AskForColor();

    uint? AskForWheelCountToSearchFor();
}
