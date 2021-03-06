﻿namespace MowLtipass.Core
{
    /// <summary>
    /// Propriétés à utiliser pour faire de la statistique
    /// En bonus uniquement
    /// </summary>
    public class Stats
    {
        public int MaxCartesRamassees { get; set; }
        public int MaxCartesTroupeau { get; set; }
        public int MaxMouchesRamassees { get; set; }
        public int MinCartesRamassees { get; set; }
        public int MinMouchesRamassees { get; set; }
        public int NbCartesJouees { get; set; }
        public int NbCartesPiochees { get; set; }
        public int NbCartesRamassees { get; set; }
        public int NbChangerSens { get; set; }
        public int NbTroupeauComplet { get; set; }
    }
}
