interface IHandler
{
    int AddVehicle(IVehicle vehicle);

    // Skapa ett nytt garage med en specificerad kapacitet.
    void Create(uint capacity);

    // Lista alla fordon i garaget.
    void ListAllVehicles();
    int RemoveVehicle(string regNr);

    void FindByRegNr(string regNr);

    void ListVehiclesByCategory();
    void PopulateGarage();

    // Sök efter fordon i garaget efter deras typ, färg och antal hjul.
    void SearchByAttr(string vehicleType, string color, uint? wheelCount);
}
