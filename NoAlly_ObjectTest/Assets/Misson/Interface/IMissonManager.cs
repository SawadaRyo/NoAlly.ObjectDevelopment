using System.Collections.Generic;

public interface IMissonManager
{
    /// <summary>
    /// 
    /// </summary>
    public List<IMissonBase> MissonBases { get; }
    /// <summary>
    /// 
    /// </summary>
    public void MissonStart();
    /// <summary>
    /// 
    /// </summary>
    public void MissonClear();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="misson"></param>
    public void AddMisson(IMissonBase misson);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="misson"></param>
    public void RemoveMisson(IMissonBase misson);
}
