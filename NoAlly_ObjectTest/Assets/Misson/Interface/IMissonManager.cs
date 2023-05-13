using System.Collections.Generic;

public interface IMissonManager
{
    public List<IMissonBase> MissonBases { get; }
    public void MissonStart();
    public void MissonClear();
    public void AddMisson(IMissonBase misson);
    public void RemoveMisson(IMissonBase misson);
}
