using System.Collections;
using Flunt.Notifications;
using Flunt.Validations;

namespace EFCore.ViewModels.CategoryViewModel
{
    public class EditorCategoryViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMinLen(Title, 5, "Title", "Título não pode ser menor que 5")
                    .IsNotNull(Title, "Title", "Título não pode ser nulo")
            );
        }
    }
}