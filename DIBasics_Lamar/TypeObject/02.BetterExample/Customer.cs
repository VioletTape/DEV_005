namespace DIBasics_Lamar.TypeObject.BetterExample; 

public abstract class Customer {
    public DateTimeOffset RegistrationDate { get; private set; }

    /*
     * Теперь можно описывать правила для определения "новых клиентов"
     * в общем виде, или же доопределять в каждом специальном типе
     */
    public bool IsNew() {
        return (RegistrationDate - DateTimeOffset.UtcNow).Days < 10;
    }
}

/*
 * теперь свойство описывается типом, как и должно быть
 * такой подход и есть TypeObject
 */
public class PrivateCustomer : Customer {
    public bool Vip() {
        return false;
    }
}

public class CorporateCustomer : Customer{}

public class DeliveryCalculation {
    public Money Calculate(CorporateCustomer customer) {
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
