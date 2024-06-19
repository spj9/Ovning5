using System.Collections;

// Klass Garage<T> implementerar IEnumerable-gränssnittet och tillhandahåller en generisk samling av fordon.
class Garage<T> : IEnumerable
    where T : IVehicle
{
    private T?[] _storage;

    public Garage(uint capacity)
    {
        _storage = new T[capacity];
    }

    // Metoden itererar över fordonen i garaget och returnerar varje fordon i arrayen.
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _storage)
        {
            if (item != null) { yield return item; }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void PerformOnAll(Action<T> action)
    {
        foreach (var item in _storage)
        {
            if (item != null) { action?.Invoke(item); }
        }
    }

    public int Add(T vehicle) => _storage.Push(vehicle);

    public int Remove(string regNr)
    {
        int idxToRemove = Array.IndexOf( _storage.Select(v => v?.RegNr.ToUpper()).ToArray(), regNr.ToUpper() );

        if (idxToRemove != -1)
        {
            _storage[idxToRemove] = default(T);
        }
        return idxToRemove;
    }

    // Hitta ett fordon i garaget utifrån dess reg.nr och returnera fordonet om det hittas, annars null.
    public T? Find(string regNr) => _storage.FirstOrDefault(v => v?.RegNr.Equals(regNr, StringComparison.OrdinalIgnoreCase) ?? false );

    internal void Populate(T[] vehicles)
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            Add(vehicles[i]);
        }
    }

    // Sök fordon i garaget baserat på deras typ, färg och antal hjul. Returnera en samling av matchande fordon.
    internal IEnumerable<T?> SearchByProps(string vehicleType, string color, uint? wheelCount)
    {
        var firstSelect = vehicleType.Equals("alla", StringComparison.OrdinalIgnoreCase)
            ? _storage
            : _storage.Where(v => v != null && v.GetType().Name.Equals(vehicleType, StringComparison.OrdinalIgnoreCase));

        var secondSelect = color.Equals("alla", StringComparison.OrdinalIgnoreCase)
            ? firstSelect
            : firstSelect.Where(v => v != null && v.Color.Equals(color, StringComparison.OrdinalIgnoreCase));

        var thirdSelect = wheelCount == null
            ? secondSelect
            : secondSelect.Where(v => v != null && v.WheelCount == wheelCount);

        return thirdSelect;
    }
}
