using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EkonomyFinal.Windows
{
    

    public class ViewModel : /*IDataErrorInfo,*/ INotifyPropertyChanged
    {
        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public string this[string columnName]
        //{
        //    get { return onValidate(columnName); }
        //}

        //public virtual string onValidate(string propertyName)
        //{
        //    var context = new ValidationContext(this)
        //    {
        //        MemberName = propertyName
        //    };

        //    var results = new Collection<ValidationResult>();
        //    var isValid = Validator.TryValidateObject(this, context, results, true);

        //    if (!isValid)
        //    {
        //        ValidationResult result =
        //            results.SingleOrDefault(p => p.MemberNames.Any(memberName => memberName == propertyName));

        //        return result == null ? null : result.ErrorMessage;
        //    }

        //    return null;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}