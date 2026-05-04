class Teknisi : Person
{
    public string Spesialisasi { get; set; }
    public string JamKerja { get; set; }

    public Teknisi(string Nama, string Alamat, int NoTelepon, string Spesialisasi, string JamKerja)
        : base(Nama, Alamat, NoTelepon)
    {
        this.Spesialisasi = Spesialisasi;
        this.JamKerja = JamKerja;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}, Spesialisasi: {Spesialisasi}";
    }

    public virtual decimal HitungKomisi()
    {
        if (Spesialisasi == "Elektronik") return 50000;
        if (Spesialisasi == "Komputer") return 75000;
        return 0;
    }
}



class KaryawanAdmin : Person
{
    public string Divisi { get; set; }
    public string LevelAkses { get; set; }

    public KaryawanAdmin(string Nama, string Alamat, int NoTelepon, string Divisi, string LevelAkses)
        : base(Nama, Alamat, NoTelepon)
    {
        this.Divisi = Divisi;
        this.LevelAkses = LevelAkses;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}, Divisi: {Divisi}, Level Akses: {LevelAkses}";
    }
}