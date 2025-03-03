using CarCare.Domain.Enums.Persons;

namespace CarCare.Domain.Entities.Persons;

public class PersonType
{
    #region Constructor

    public PersonType()
    {
        Type = TypePersons.User;
    }

    #endregion
    
    #region Properties

    public int PersonTypeId { get; set; }
    public int PersonId { get; set; }
    public TypePersons Type { get; set; }
    
    public Person Person { get; set; }

    #endregion
}