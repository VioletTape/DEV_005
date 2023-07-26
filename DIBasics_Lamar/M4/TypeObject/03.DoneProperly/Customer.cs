namespace DIBasics_Lamar.TypeObject.DoneProperly; 

public abstract class Customer {
    public DateTimeOffset RegistrationDate { get; private set; }

    /*
     * теперь можно тестировать и нет явной зависимости от
     * системной зависимости с текущим временем. 
     */
    public bool IsNew(DateTimeOffset now) {
        return (RegistrationDate - now).Days < 10;
    }
}

public class PrivateCustomer : Customer {
    public bool Vip() {
        return false;
    }
}

public class CorporateCustomer : Customer{}


public delegate DateTimeOffset GetUtcNow();

public class DeliveryCalculation {
    private readonly GetUtcNow date;

    /*
     * Время передается как зависимость через конструктор
     * сервиса расчета доставки. 
     */
    public DeliveryCalculation(GetUtcNow date) {
        this.date = date;
    }

    public Money Calculate(CorporateCustomer customer) {
        if (customer.IsNew(date())) {
            
        }
        return new Money(23, "TTR");
    }
    
    public Money Calculate(PrivateCustomer customer) {
        return new Money(23, "TTR");
    }
}

public struct Money {
    public Money(decimal amount, string currency) {
    }
}
