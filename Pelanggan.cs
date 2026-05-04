class Pelanggan : Person
{
    protected decimal _total;

    public Pelanggan(string Nama, string Alamat, int NoTelepon, decimal total)
        : base(Nama, Alamat, NoTelepon)
    {
        _total = total;
    }

    public virtual decimal HitungDiskon()
    {
        if (_total > 100000)
            return _total * 0.1m; // diskon 10% jika total > 100.000
        return 0;
    }

    public override string GetInfo()
    {
        return $"Nama: {Nama}, Alamat: {Alamat}, No Telepon: {NoTelepon}";
    }
}

class PelangganReguler : Pelanggan
{
    public PelangganReguler(string Nama, string Alamat, int NoTelepon, decimal total)
        : base(Nama, Alamat, NoTelepon, total) { }

    public override decimal HitungDiskon()
    {
        return base.HitungDiskon(); // ikut aturan induk: 10% jika > 100.000
    }

    public override string GetInfo()
    {
        return base.GetInfo() + ", Tipe: Reguler";
    }
}

class PelangganVIP : Pelanggan
{
    public PelangganVIP(string Nama, string Alamat, int NoTelepon, decimal total)
        : base(Nama, Alamat, NoTelepon, total) { }

    public override decimal HitungDiskon()
    {
        return _total * 0.20m; // fix: VIP selalu dapat diskon 20%
    }

    public override string GetInfo()
    {
        return base.GetInfo() + ", Tipe: VIP";
    }
}
