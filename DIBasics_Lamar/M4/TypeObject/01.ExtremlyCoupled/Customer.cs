using System.Data.SqlTypes;

namespace DIBasics_Lamar.TypeObject.ExtremlyCoupled; 

public class Customer {
    public bool IsNew { get; set; }
    public bool IsVip { get; set; }
    public bool IsPrivate { get; set; }
    
    // остальные методы не важны для примера
}

public class DeliveryCalculation {
    public Money Calculate(Customer customer) {
        /*
         * ужасная ифология, в которой черт ногу сломит
         * Такое очень трудно:
         * - читать
         * - понимать
         * - изменять
         */
        if (customer.IsNew) {
            if (customer.IsVip) {
                // ... 
            }
            // ... 
        }

        if (customer.IsVip) {
            if (customer.IsPrivate) {
                // .. 
            }
            else {
                // .. 
            }
        }
        else {
            
        }

        return new Money(23, "TTR");
    }
}

public struct Money {
    public Money(decimal amount, string currency) {
    }
}
