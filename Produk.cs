class Sparepart : Produk
{
    public string Merk { get; set; }
    public string Kategori { get; set; }

    public Sparepart(string kode, string NamaProduk, double Harga, int Stok, string Kategori, string Merk)
        : base(kode, NamaProduk, Harga, Stok)
    {
        this.Kategori = Kategori;
        this.Merk = Merk;
    }

    public override double HitungHarga()
    {
        return Harga; // harga normal
    }
}

// Polimorfisme — ProdukBekas hitung harga berbeda
class ProdukBekas : Produk
{
    public string Kondisi { get; set; } // "Baik", "Cukup", "Rusak Ringan"
    public double DiskonPersen { get; set; }

    public ProdukBekas(string kode, string NamaProduk, double Harga, int Stok, string Kondisi, double DiskonPersen)
        : base(kode, NamaProduk, Harga, Stok)
    {
        this.Kondisi = Kondisi;
        this.DiskonPersen = DiskonPersen;
    }

    public override double HitungHarga()
    {
        return Harga - (Harga * DiskonPersen / 100); // harga dikurangi diskon kondisi
    }
}