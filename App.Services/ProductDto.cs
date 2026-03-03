namespace Services;

public record ProductDto(int Id, String Name, decimal Price, int Stok);

    //İnitler sayesinde dto olarak dondurulen listelerdeki verilere müdahalede bulunamayız yani degistirelemez Immutable veriler//
    
    //Bundan dolayı da record kullanacam
    
  