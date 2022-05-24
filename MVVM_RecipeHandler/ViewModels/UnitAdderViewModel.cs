using Microsoft.Practices.Prism.Events;
using MVVM_RecipeHandler.Events;
using MVVM_RecipeHandler_Common.Command;
using MVVM_RecipeHandler_EF6._0;
using MVVM_RecipeHandler_Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MVVM_RecipeHandler.ViewModels
{
    /// <summary>
    /// Displays the students data in a list.
    /// Derives from <see cref="ViewModelBase"/> class.
    /// </summary>
    public class UnitAdderViewModel : ViewModelBase
    {
        #region ------------- Fields, Constants, Delegates ------------------------
        /// <summary>
        /// new Unit from textbox.
        /// </summary>
        public string newUnit;
        #endregion

        #region ------------- Constructor, Destructor, Dispose, Clone -------------
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAdderViewModel"/> class.
        /// </summary>
        /// /// <param name="eventAggregator">Event aggregator to communicate with other views via <see cref="Microsoft.Practices.Prism.Events"/> event types.</param>
        public UnitAdderViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // load ingredient data from db
            this.LoadUnits();

            // hookup command to assoiated methode
            this.AddUnitCommand = new ActionCommand(this.AddUnitCommandExecute, this.AddUnitCommandCanExecute);

            // subscribe to event
            EventAggregator.GetEvent<UnitDataChangedEvent>().Subscribe(this.OnUnitDataChanged, ThreadOption.UIThread);

            // Image img = Image.FromFile("../../MVVM_RecipeHandler/Images/Download.jpg");
            string ImageString = BuildImgString("../../MVVM_RecipeHandler/Images/Gnochi.jpg");
            string ImageString1 = BuildImgString("../../MVVM_RecipeHandler/Images/curry_roux_cut.jpg");
            string ImageString2 = BuildImgString("../../MVVM_RecipeHandler/Images/yuxiang_eggplant_cut.jpg");
            string ImageString3 = BuildImgString("../../MVVM_RecipeHandler/Images/double_cheese.jpg");
            string ImageString4 = BuildImgString("../../MVVM_RecipeHandler/Images/eierspeis.jpg");
            string ImageString5 = BuildImgString("../../MVVM_RecipeHandler/Images/Jap_curry.jpg");
            string ImageString6 = BuildImgString("../../MVVM_RecipeHandler/Images/Palatschinken.jpg");

            RecipeContext context = new RecipeContext();
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString));
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi1", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString1));
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi2", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString2));
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi3", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString3));
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi4", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString4));
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi5", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString5));
           ////context.RecipesSet.Add(new Recipe("Trüffelgnochi6", "Schlagobers auf mittlerer Hitze auf die Hälfte einreduzieren mit halbem Rindssuppenwürfel Salz und Pfeffer abschmecken. Gnochi nach Anleitung kochen mit Sauce übergießen und mit bevorzugter Menge Trüffelöl abschmecken", 1, ImageString6));
           //// context.SaveChanges();
            // string ImageString = "/9j/4AAQSkZJRgABAQEAAAAAAAD/2wBDAAkGBxITEhUSEBIWFRUXGhgbFxcYFyEYGBscFx4bFxsYHhgeIikhGiAnHhsYIjIiKCosMC8vGiA3OTQuOCkuMSz/2wBDAQoKCg4NDhwQEBwuJyEmLjAuMC4uNzAuLi4wLy4uLi4uLjAuLi4uLi4uLi4uLi4uLi4uLi4uLi4uLC4uLi4uLi7/wAARCADCAQMDASIAAhEBAxEB/8QAHAABAAICAwEAAAAAAAAAAAAAAAYHAwUBAgQI/8QARRAAAgEDAwEHAQYDBgQCCwAAAQIDAAQRBRIhMQYHEyJBUWGBFDJCcZGhI1KxFSQzYnKCksHR8AgmFhc0NUNUc5OisuH/xAAbAQEAAwEBAQEAAAAAAAAAAAAAAgMEAQUGB//EADMRAAICAQIDBgMIAgMAAAAAAAABAgMRBCESMUEFEyIyUXFhkdEUFSNCUoGh8LHhMzTB/9oADAMBAAIRAxEAPwC8aUpQClKUApSlAKUpQClKUBrtb1aC0hae5kEca9WOT14AAHJJPoKrPvN7aW9zB9ht1FwlzHkTK3kRwQ0a8DlsryCRjjPrUl7f9ptMj3WWo4YvH4gRkYocFtmXUeU7kIqmOzVgI13JKHz99VIKZzwRjkcVVbPgRt0Ol7+zhfIy2UF0GtWMiqbWIrAwQNsdmLcqchyNxOceg9q9nZi+P9pQ3urSytNksJN6xpEkakEyLt5U5ACLt6nqWxXqrS6pG11KlpbRvLPuXgDMa5IOX+PfPABNUVXTlLB6uu7P09NTktn0+J9DdmNfgvoFubYkoxYeYYYFTggj09/qK51DtJZwP4dxdQxOF3bXkVWx0zgmq+HYjWp0WObUIbOFcAQ2aFFC/iHl289eMkVtNK7n9Nj80yyXMhfeXlc5J9iFwCM8nOcnrWw+dO+pd69krLHaJNfSEMStuhbaF9SSB6+wOP0zrXbXtTKrt/sqDbuLhvElcnlRgFWGOMjy+uc9KndrpdlZq0kUMFsuPO6osQx6bmGOPzqu+23eg5eW00pRIwABulcFELYztGCGwMjdng54OKN4JRi5PEVlmo7U9mdHsjjU768nuSoK4ctIADk7eCEDHI8598e9QrTra6MjyW89xbRMCF3SlpChG3a2NoPGfQY9PetpZaeVdpppXmmbG6SQ7m49icn9/SthWOzU9In0Gk7HWOK/5fVmmTQAhD280sUo6SK53Z+mOvxU77A95EyzCy1YjcxxFcHCqfQK2AByeA3zg+9QrUdbiiBG4O4/CPn3PQVo9SNxdL5lRE6qD16fzYzz9KlTKz83Ip7Rp0qWKvN6LdfufWWaV81Xna7V3KF7/wAIJjYI8KpIwAG48/T8Rbr0rEvarVc7l1RyxB4ypGPhcED8wK05PH7qfofTVKofTe9vU4uLm2juBtABTMbbh+JiNwOecgKPjFY+0HfHfuhNvClqvoz/AMSQ49ACAOT67elMkeCS6F+0rW9npJmtYGuceM0aGTAwNxUFuPTmtlXSIpSlAKUpQClKUApSlAKUpQClKUApSlAKUpQFb962mNI0P2bTYru4mDR+LIMiJV8wJ5AH3mIJIAPvnFVdZzGJpo3KMsJAklhT+7qx4Chx94k8DgZ2nGa+mCKj1/2LsJYlha1jESyeL4aDw0L4K5YLjdwT1qE4KawzTptVPTy4olFs8l1NDaWTZllYZZeQidSx9sA5PwPkVdHY3sna6XDtVlMr7fFmc4aRicDqfKMnAUfueaqPsjc/ZIZCJEt4bia9t2u8YkXZCGh2lTlAHycL1P5DM37GdnnvRFf3cQSOS3ijaJ2Z3mMO1o588eFyuRgtkHJ60rgoLCGq1U9RPil8ixtO1CKdPEgkWRMsu5TkZU7SPoRWHW9ZhtIXnuHCIgJOepx6KPxEnAA+RWs7RdpLLTYGeQonLMIkwHd3JY4QerNkljxySTVG3Ut1qEiXGovvA3+HFt27AxzjgDjp1ySAM12c1FZZHT6ay+fDBf6Nl2n7S3Grv5gYbFWBSIjDyY6M5H14BwM+pGaxW1qkY2xoFHsB/wB5rMBWt1fWFg8uCzkEgDoPlvj/AKVhlOVrwj6mnT0aKvilz9foeu9vEiXdI2PYep+APWo5eXM1znbmKPbwpP3j8+4/asdrZ+IPEmYuzfPT/v26CuweSLhhujGeR94D0z74q+ulQ9zytVrrL9uUfhz/AHOlj4QPhlAr9CCN2fyPtWWSwRfMHaMeuGwMewz0rM4ilGDtb14PP7c15pYII8FySfQElv0WrjFhJdDEGtwfxSH0HLE59s8Zo7Wx942z6AqwPT8q9K3h6RwvwPYL16VybiXo0GfyYEfvQ5t/UdEgc/4dwSPnDfvXXT2ngmS4ZYpTGcqJiTGG9G2gjocEDpkdKxt9nJ88RT5KlRn2yKxtEEdZBJFKB0WYllPGOQD5q6iE1lf7Lz7oNY1C9jnur2TdGzKsKiMIvlB3upxkgkgc55Bqx6rDuf1vULwSPcGP7JGoji2RCMM4xnbj8KjI9sn44s+pGUUpSgFKUoBSlKAUpSgFKUoBSlKAUpSgFKUoBSlcGgKW7oey9tcNeTXUCTGO5dY2PMWerbYj5f5eSOmB6VLe3neHDYE28aNNdFMpGq5Rc5Clz7ZB8oyePTOaqm0128tZr+zsGTdJcMzXKHhVBIwq8quc9Rz1A6ZHGl6SkGWBLO33nPU85NVWXKHub9F2fZqXnlH1+hii09pZWu70iS4kbeT0Cn0AA444HsMCtriuD7npUb1fWt7rHbybR+KT0JPoD7fPzWNKVsj6OUqNDVhL6s9GraycmG2G99rEsvO0KCzYA6kKCfjFa/SraMLvU7iRgk/uMVseyEPg6lYhAGO8hscE7lZSTn0AJ4+CK2/bnsabBjdWa5t2IEkfJMfU7gf5OOp6Z9umuKjDwo+fu1Fl1nFPkunoRZo2hJZOYycsoH3R7jmvZBcI4yhB/r+lILlHGVYH+o/MelYp9PjY7iMHOcgkGpe5FJry8jrNYw/iAHPvjOfTr+1YllgiJ2cn/Llj+WfSso0yLqVyfUkk/U1j/tCJfLENzHhVQdT0A46n9a6ce27wjv40znCp4Y/mbk/ltoUuPRkP09v+tdtYtL2AxLcxG38XJQnBzjjBwTgjI4ODyK8Rhm3splbO3I64YDrj0HOBQhx+56/tbYIkhYn12jcteCRYAQ6SBSDna0e9cg9ChGCPg5FZLKa4LRpGDMXBKooLMcZJAwMkgA+/Suby4gYlZlZHBwwKlXB9QeP610i5KSw/5Lh7o7rVp3aa6lP2MKVRGhSLe2eqKqgqq889DnHpxa9fO/Ym6128dUs7u4ECsN08wDRgA+hcEyHr5QeeM4r6EjBwATk45PTPzj0qRmZkpSlDgpSlAKUpQCsFzOsaNI52qoLMT0AUZJ/QVF+13eFY6e6xTs7SEZ8ONdzAehOSAP1zXW51iHVNLujYN4heGWMKRtYOUOEYHoeR8c9aAiFt2z1nUA0unQwQW24iNpsmRwpx8jn4GAQRk4qZd2/ah9QtTJMgSeKRoplX7u5cHIyTwQR6nnNRfurvlk02EKCDFujbIx5lO7+jCsPYXw7TXL22z4a3EaSQx/hcgbnI56g+Jx8N0xVULG5NMvnUowUkb7tn2+e3uFsbG2N1dldzLnCIp5BY++OfQcjnnFYOyPbi6kuxYanbJbzOheFkbKOBklOp5AB6H8J6cVog3/ma66p/d14I/wAXCxcj2A+P5T8137wmENzpt7zmO5VCB1Kyct+yn9aOzE+E4q8wci36UpVpSKUpQCuCK5pQHzQ1kbC9uLGUgDfujbBG9X5GCevHH5hhzWyYgAknAHJJ6CrM70exx1C3BhwtzCd0TcDPvGW9Aeo+QKpntTo+pxRI99atDAGVXZWVsk9C21jgfmMZIHXFZbKOKWUe5ou1FTS4SW65Hg1nV/FbwY22xk4Zx+L4/wBP9fyro+mKz28G7YHkWMtt3HLlV3bRgtziuL6BDBlAMDDDH6E/PGa4064cTWZlyhE0Lq38yFwNwPwVIPyD6g1dGKisIw32ysk5Wbt8voZ1t5be+t4LpP4sU8XXoyllwQR1HAIP69DX0FKgIKsMgggg9CDwQar7vO7R2SSRwtB9pu4nVo1BK+GxwVDMvJz5T4Y68dOK8UbdqLkbo4PAVixGVjjK7c+XEmXwTwMjnjnFVyhKxJ8iEbIwbT3NjrfdfBJIslrL9lAUqVVNwJySG5YEdcEewGMVq4u6q4KkyahiTPl2oSmPnzA5+n610g7e6jZTLDq9tgMU8+3YVBHLDblZPcgc8EfAtC2nSRFkjcMjDcrA+Ug85zUJSshsyyHdz5FR6x2KtbJRJqV/JICCRDGu15CCBhSzHA5GeBj3rpoXa7YSNI0bO0AM+Hlkyc7SzqvGcn15rL2W0v8AtvV5pZmd7SEltpORs3YiiHI2hsFjj+U++avtEht4wBsijUBR0RQAAqj0HQAVoUcrxGSU99ihNb7apdKbHWLR7Ugg+IgJeNhnzCN1zgjynBPBNRK7hkt3EcpG5QCrg7lkhkwQVI9COQfgjqK+me0/Z23v7doJx5Wxh1+8pByGVv8AsGvmrWIzaG5064QOYpcxSBuUbj4wUdCCV482D6V1RS5DvHnLJL3NWby6nC6sAtvC7OccYbdGq59D5gc/5TV5XPZmyllM8trA8jDDO0asWBAHJI54GPyqgOw/YxLiI3d7P4NsT4Yw4RpG3BdpJ4CZ4x1J9sZqe92s0llqdzpRkeWHwxLDufeYwMcHptyGHGB0X3opLODkovHEy1bW2SNFjiRURRhVUBVA9gBwKz0pUiApSlAKUpQClKUBTHYl4zqmq+MCboTvtZh5vBDFQFz0H3PzBWuNDuU0vXHjcqlvfKGU5YKj5OAc+Xlww+N69Olejt6RY63bXvmSG5Twrh+qFh5QD7EARn/Z+dervS0oz6dLsUM8eJF45AU+fHr9zd064qlvhs9zTFKdfxR4Oweba+1HT5Fw4ladSDlSjkAenHDRn6n2rjtk/wBi1Kw1POU3+BKG+6qvuG4eoO1pD/tHua8M2pRNqemalF5VvomilxxmVP4ZDe/mMY/2ipD3m2XjaZcD1QLIOMnyEE9OnGRn0yahLw2p+pKHjqa9DBdxhe0su9txe1Ux452AbQVPtnax/wB3zXl74oj9ijlB4huI3PvjDLwffJFeDTNVMuqabOrqWuLDbMFGeY/Ezz774x8jaRW572oS2lzbQTtaNjj0AYZP5DNJ7WoV71MtWGTcqsPUA/qM1lrXaDciS2gkDBg8UbZAwDuUHIHp16Vsa0mQUrBb3KPko6uAcHawbB9jjoaz0ApSlAK8Or6fHcQyQSglJVKNg4OGGDg+hr3Vink2ozdcAn9BmgPmjtp2bk0y4kt28RraQfwJSMjzA+QtgLuHOQPQA45Nbvs1Y21xohmuE3PZvK6mPCSLsIl2FiCCGzzkHqPUZriysJtYtpL7UNQeFDIVSPGLdCCAnDMFbltvv85qN6x2fvrOb+zkmLLcDcoRikc3BGCpP3srt2nqduOoqDabwmXLiSTa2J33G6AJ3n1O6QSOZMQuzbiGGTI2PQ8oATzwcYzVpat2ls7aSOK5uI4nk+4rHBPOM/Azxk4qv+4LW42tZLFsLNC7OF6MyORlsf5XJU+3l+Kk3a7u7stRuI7i48QMi7SEbCuoJYK3BIwSemDz+VTKTb9quzcF/btb3C5B5Vh95GHR1Pof6jINfOel9odRshNpsBDESOmAhkYMCY3EePQkZ6H3GMmvqC4nSNGeRgiICWZjhVVRkkk8AAetUX3bFrnVr+/HEZMoBBAGZZAVHHDeRevqcHqahNpRyydablhG1/8ADpOoS8hwQ6vGxz7EMmMdcgqc/mKm3eT2LOqQRxCfwTG+8HbvU8FcEZHv1qtu1WgXGn3i6ppgYguDJCik4zy4IGcxvg548pPHpiU6L312Eif3pZLeQdV2GRSQPwsoz1yOQK7GSkso5ODi8MnfZrSfslrDbbzJ4SKm8jGdvrjnA9h6VRXaEC61y9YIsiQxT583l/hQGME5B6OcY9D68VIu0nfMZv7vpFvI8r7l3uvP5xxqdxOMnJxjHQ+mLsn2Ra0sbya7UtPNDLvXcCQmwnaTnBcnJJz7e1cnJJHYQcmaB7xItC04ndtN0GcDG4hHmZsZ49BjPxmp53RadNNJc6xccNdHbEmMDw0IAb67VUf6ScndVc6gm7QLBWIVWuWDORnYC043fl1P0r6L022SKGOKPGxEVVx02qABj6CkVzZ2x8l7HspSlTKxSlKAUpSgFKUoCkklk1PS7vT713a/sS8g9Gk8Pdsbp5wQSnvyp6nNSTsRqwvLCGRmDsU2S++9fK2fYkc/7s1oe3EctjdRaxbbsKVS7UMAHjyqrwRznhfghDx1rDotwlnqjJEUNnqKia3KnhWwSVx0BzuXb/o+RVE/xIZRpr/DnwvqR290KVI7zTc4a2Y3tkSc5hXcsihvwnbtbHqyH86tPs5qgvLSKfG3xU8w64PKsPnkGoj3o2piktNQWLxfBfw5U588cmQFOOgOXXp1kH5Vsu7CwngsQk6NHmSRo42BDIjYwGB5BzuP1qFklKCl1LK4uM3HoV7od2Ip9Ot94WW1u54myCGaOVkwcexPij/cPpZXeZGG0u6yzLhVPl9cOpAPwehrYDQLX7UbzwgbggDxCScYXZkDOAdvGcZrJ2htjJaXMYON0MoyRnGUPp61x2KUkyUanGEke3s7rUVto1tdXBVI0t4S2wZA8qqAB7k4GPc1AtLgu9YL3d5dTRWrO4gt4j4eUHCsSOv5kEkg9BiozpmpNqUOm6WgcRxBmulDgKyLJwT65CjOPQuMfFywxqiqqjCqAAPQBRgD6AVZbY47LmUU1KW75Faa5pA0aa2u9NlaMPIkUluzFhKDyevJ44PsSCMVe+Kp7sJp/wDamoS6lPhre2cx2ic7SyEES4P0b/Uw6bRVwirY5xuVWNOT4eRzSlKkQFdHUEEHkHg13pQFD9qtFl0nx4vCMuk3RCnzZeBnHJUHJyuMgkYOEGc1ro79tQ0ltrZu9PZZEf8AGY05D5PU7Qcj3jHuK+hXQEEEAg9QeQapbVNGi0nWoPBYC3vw6PERhUyR5R6bd5XA9ASOlQktsoshLozSPptz/B1vRgwaQO88QO4h9x8UBTjxI2YHyjngEfG8tu/CRBsutPYSAAeVyu58DI2smVznOOcZFOwshsdQutLcnYT4ttknG0+YoueSdp5x6xtU7v7hI43llxtjVnJIzgKMkj5wKqdri8YyXKhSWc49Stdc7Q6xrKC3htDbWzNtkYk4PI++7AHA9Qo5/app2Y0i1sYTBC65BBlJcFi+ACWGfL8DjivfoeqJdW8dzEHCSAkBxhuCVORz6g9K0Ws93dhcvJI8TJJISWdHI8x6ttOVznnp6moSs4tpbFkKuBZjubHWe1dlaDM9wgOMhFO9z+Srk/U4HzWu0iystQjF5NpyozFseKg3OMjEhx97PuR7445PfR+7/T7dlkjg3OvIaRi+D77T5cj3xWftP2xtrMbSfGnyqrbxsDIS3TI5Kj6eo45qO3KGSbb5zxgyfarGzmgtQkcDyhvCwgVTg/d3+5JOBnk/mM+/Xlza3APrDL/+je1QH/0Yv9VPj6k5tVXd9nhRfMjH8TZ5HQeuT/l4zKr5JLfS5luZ/EkSCUNNt6kqwU7fU8qOevU12UVlb7nIybT22K51Af8Alq2IBwJ+fY+eYc9MLn98fnX0XbfcXgDyjgdBx0HxXz92X7Iatf2VtbOY49PYmTf5TIAGY9PvZJLYHA96+hI0AAA6AAD6VqisGGTzgyUpSpERSlKAUpSgFKUoCseyWsLqNkftCLv80VxFjow4OVPTIwcHpz7VWep6VdxTRaTHGJGim8a0lOAREdxYEjkLuAZueChwORU+7wYBp2owalGjCCfMd4VGVzwEcj0Pr/s9zzL8g4I5BHB+DzWZt1PbkzZFK2KzzRyx98f8s/FcZrigrOazFeXSRRvLKwVEBZmPoB/X8qgV12iudUl+zaUxhgTBmuSMHzA+QKcH3GOpx6Ac5e1+dQ1CDTVZhDEPFusHAI8pVffIBH/3M+manNraxxII4UVEUYCqMAf9+9WLEFnqUvM3hcjR9jeyEOno4jZpHfbudgB938KgdFzzjJrx94WuSxpHZ2iM1zd7kjKttKDgFs+h54PGME+lSyob2sG3VtHcYDGV1J6Eqdgxn6tx/mPvSvxT3FvgrfCWD2J7PLYWUNqp3FBl2/mdjuc/lknHwBW/pStp5wpSlAKUpQCoF3x6AbrTnaNQZYP4qHGWwgJdVI5BI5+do+KntdWXIweRQFBdpdX8S20/WYpE8eHCyJnG8E7XUAndw24flJmpp2ltZL/Tylm6p46xkM+R/DbDEHAJBK8fqK9T90GlGYy+C+DuzEHIiy3qB94Y9ADge1Rru7d7We70mdstbvviJbOY3CkAD0GCrf7zVFkcLK6GqqzilwvqdD3aeGALPUbuDGMjfleOCQFKYovZvWo8pDqiMgzsMq7nIb+YlWPHpyfpU8NcZqjvJGjuY9CDHsHdygJe6pPLFwWjUbQ3uCSx49sj6VutB7FWFoQ0MIZwciSQ73B+D0X6AVIK4NcdkmSVUVvg5LVpO28gGnXZJx/BkGflhtA+pIH1rdVF+9D/AN13PXonT/6ideOnvXIeZCzyMlfdbGV0mzDLt/hA4+GJYH6gg/WpXWi7ErjTrIcf+zwfd5H+GvQn0re16B5YpSlAKUpQClKUApSlAantNokd7ay2s2dki4yDggghlYfkwB+lVl3fatLGW0q/BS6gzt3HPiR9Rtb1wD9Vx7HFxVXfe72aeWFL+0X+92hDhl+80a5LJx1x94D/AFD1qE4cSwTrm4Sybc0Fazs3rsV9brcQnGeGTOSjDqp/qPcEGtnWFrGzPUTTWUarTdDSG5uboHc9wVzkfdVVA2g55BIz6eg9Mna0rkCjeQljZHDMACzEAAZJJwAB6k+lQbQUGr6v42GNpYfcO7KSTBiVcY6g4zweiL/Niumqx3eq302lxOsFrBsNw3WRwcNwPz4x04ySeBVr6Jo8FpCsFtGI41HAHU+7MerE+pNaqa8eJmG+7i8KNnSlKvMwpSlAKUpQClKUAqq++TR5IhFq9qMT2xAkwud8bEAbsHouSD8OfarUrzX1qksbxSruSRWR191YEEfoaAimn3iTRRzRnKyKrA/mM/8A8rMKgvZcyabfSaTcNuiYGW0ct+Ak4TnHJw3H8ynHWp2RWCcOF4PUqnxxyRmW31fO1J7PbuPnMb79p6eTO3I/PmvXa6JIxDX8/wBpZJFkiATwkQoMDyqTuOTu8xOCBit1mlc42dUEKg/fG39xjjzjxJ41znoMOc4/FyBxU6VagdtH/bGrxrGT9k08h2kUDzyhgQob2JUfRG9wanTHMivUSShj1LgtIQiIg6KqqPToMdPSvRSlbTzhSlKAUpSgFKUoBSlKAVwRXNKAqrVuxV5YTvd6PiSKRi01mxCr0JJjPpz0HUcDkcDP2a7XW91bePIyQMC4eN5ACpj5J5wcYIPTit73q6nPbaZPNaNtkXZ5vVVZwpIz684+tQ/sx3RWVxCt1czzXDXCLIG/wsGTDlsckkknrxyePWq51KRbXdKBl1DvJ06MYika4ckBUiRsnJxwWAH715l1TWr0+HZWDWaErme4GCBnkhXUA+vRW4/MVZel9l7K2x9ntIYyMeZY13cdDuxkmtziuRpijstRNlOR912qNLJctqqxTyAK7QoV3KAMDKlP5V9Pr7ybug12W4tZIbt2a5tpXjlLnLdTt5/Vf9tT6qo7LbbbtJf26Eok0Ql2MSA8h2OxXOd33pDn08wHAq0pLXpSlAKUpQClKUApSlAKUpQET7edi4tRiVS5imjO6KZR5lPsfUrnBwCOQDUCl7V3WmMLfWYWfP8Ah3MIBDgEjzA4G7jOBg4IyPU3RUE76NKE+lTHIBhxKpIz93gge2VJFRlFS5k4TlF5Rp5u8LS1Tf8Aag3JG1UYtx/lxkD5PBrSX/e7ZruEMM0hHQnbGp+eSWA+lbns9oOnXNrBcGxtwZI0YgRjG4DDfTOf+dbZOzdiCCtlbAgggiFMgjoRxxWT8NPdM3fiyWU0U12l7x7q7RoRshifqqcuR/KZD1BPsBnp0qef+HK+Gy7t/UNHIPyYFD+m0frU2l0+F+HhibjHmjU8HqOR04HHxUJ0e7tdL12ZZGS2t57dSvG2MPkYAxwg8snX1z71fVZF7JGa6qUVlvJdNK0Fv2y06QgJfWxJIAHjLkk9BjNei27SWcmfDu4GwxU4lU8jkjr7Z/SrjObeleO21KCRd0c0bqOpVww/UGs8UqsAyMGB6EHIP1FAZaUpQClKUApSlAKUpQGm7WaW1zZXNumN0sTou7puIO0n6459KjnczqAk0xIirK9szwyBju8yndwfbDDj06elTuqh7J6mmmarf2UzNHDK3jw7hkZbk4I5OQcDr/h84odSbeEW7QGoFrHeOi+W2jMhx99sooP+nG4/tUc1Dt1ey4CMsIxzsGSc/LZx9KplfBHoVdl6izfGF8S3ncDrVR952sxW+o6ffxukngM0cyhuQsg4ORx91pPrj3rQ3d/PKAs00kgHQMxYfoa1WsWgeB0xnykgD3HI/eq/tOWlg2PsWUa3Jy3xyR9Iqc8iu1Q7up1b7Tpluxfe6L4chJywaM4wfnbtOfXIqY1qPCFKUoBSlKAUrHLIFBZiAAMkk4AA9SfSoJqferYxyeDbLNeSgkMtuhfAUctuOAw+Vz60BPqVU2r9vNZZP4GlNbq+SksuXKr7tGANrY5wf3qEX1zfXWReX0jxudzRJ5Ez0AGPw49MfvzUJTjHmaaNJbd5EXprfaqytOLm5jjPPlLZbgZ+6Mn9vUVX/aTvesZreWG3t57gyIyEFNi4cFSSfMfX+WoLBo1uuSU3seSz+dj+tbFSBwOKoepXRHq19hzfnkl7bnPYPt9b2VmLa7WVXjLbNqfeVyW9SMEEt1+K31x3r6ePuLPJ74jAx/xMKj7YPUA/mM1woA6AD8hiqnbBvODQuyZrZT29j03/AHg385xY24gUA5aYbi2eBjIAGOuOefjroptHeaTxr24ed8Ac8DAyQv8Ap5PAx1NbUyV7tP0e6nXdBA7r/MMBfoWIBrneSe0FgujoNPV4rZZ99l8jSSaLbEYMKfQY/pWJtAtT/wDCH0JH/OrAsO7y6kAaV0hz+E+dh9AQP3qS6d3d2qf4xeZsg8kqvHptU8j881KNdr6ld2s0MOib+CKn0ju+W7Y/Z4cKuAzFyqD1wepJ9cD4qeW3c9CiARXt3E4IIMcm1FP4tq9Rkeu70FWPa2qRqEjRUUdFUAD9BWetUIOK3eTwNTqI2y8MVFfD/wBK2/8AV1fREiy1q5jjOPLKvjNn1IbcAAfYAfWj6H2jhwYtSguMeXZLEEG3H3iVXJIPz/0qy66M2OTVhmK3tu8K6tT4WsafNGy5zcQIZIGA/H8D4BJ56DpU10PXra7jElrMkqkZ8p8w+GXqp56EVX3bLtk0zNBbtiEcM6nl+OQPZeo+fy618+jKHEtuxglUgpJHxgj4BH5VQ9RBSwepDsm+VfGsZ9Op9L0qgF7Y68nlW5jcDozRJuP58Uqfew9TP9g1H6GfQFKUqwxnBqlu+exEWoWd0AQJVaJyD1K/dGP9/wC1XRVbd+9kG04T8B4JY2Q5wRvOwge/VT/tz6VySysFlU+Gal6MgmKV1hlDKrDowB/Xmu2a8k+8i8xyjmgroXr12Wm3E2PBhkfPQhTt/wCI+X96JN8jk5wisyeEbjuQ1ERT3lizKMuJYgeGbIw4HvhRHx6c1clUFqOkXWl3dnqlxGohWQJIAdzKJAylm2j+VmxyeQB61fSsCMjpXqwbcVk+H1KirZcDys7GSlKVIoOKrjV+3NxdTtZ6FGszrjxbpj/AiyccHo56/pwG5xH+8/tk11IdOsJP4Yz9pmXPUH/CVgcHoc9c5x6Gt53f9obS3hW1MaWwXowyVcnqzMeQ3yTj9MCDnFPDZohpbZ1uyMcpBO7Oa5wdY1Ga6AxiJP4UWepzj73OecA9KnOj6JbWqBLWBIlHHlXB9+W6n6mvfG4IyCCD0I5FZKmZzjFeG80qCXHiwxvjONyBsZ64yOK91KYOqTW6IlP3fWLMWCOmfRXIX6A5xXWXu8siu1VkU/zCQk/ocr+1S+lQ7uHoXrV3r8z+ZD4u7yyCgMJGIPLGQgn4IGAB+QFe6HsXYKMC2U858xLH9SScfFSLFMUVcV0EtVfLnJ/NmusdGt4eYoY0J9VUA/r1rYAVzSp4KJScnls5pSuKHBXFee9vY4kLyuqKOpY4FQ7XO8OOPyWyiZsffzhAf6t9OPmoynGPMup09trxCOSZXV0kal5HVFHUscD9TVVdtO1bXD+HA7CAAgkeXeT1J9dvpg9efitBqWpz3B3XEjPg5AP3Vz7KOBXmArFbqHLZH0Wi7KjU+Ox5f8I4ArtSlZz2RSuM0odPoalKV65+eitdrWmx3EEkEqK6upG1xlc9QcfBwc+mK2NKA+euwnZe6m8a1AQNbSNHKxfgEk9Mcn1xx6VYmmd20YybmYyeyoNg+pySf2rWWRNl2jkiVR4WoxiTAP3XjDMWI+Ssn/H9KtKq+5hnODb946hVqtSwlttz+Zo7LspZREMlumR0LZc//lmt0iADAAAHQDpXYCuamklyMkpym8ybfuRzt9oP22wnthjcy5QscAOh3KSR0GRj61ru6PXzeabE753xfwXJOdxjAw2flSv1zU0NVP2IRNO1q80/diO5Ant1xhQfMxQDGOFLDI9I66RLYqAd6XbCSzSO3tMfargkKSMiNBw0mOmc9M8cE84wZpql/HbwyTynCRqzsfhRmvnaG7kvLiTULgANLjYoJIRQNuBn04/c+9V2T4I5NWj0z1FqguXX2Mul2Ihj2Z3Eksze7Hqa9RFc0rzW23ln2ldca4qMeSNhpGvXNsV8KQ7FOfDJyhz1GPT6VLNO7yTvxcwgJjrGSSD6eU9R9agVMVONs48mZrtBp7t5R39VsXDp3bWym4EojPtJ5P3PB+hrfxzqQCGBB6EHIP5H1r5+K0OeBk4HTnp+XtVy1T6o82zsOLfgnj33+h9C5pmqDm1Cdsbp5TgADMjcY+tZRrV1x/epuOn8Rv8ArzU/tS9DO+w7P1L+S96Zqiv7fu//AJmb/jNI9fvFfeLmXd8tuH/Ccr+1PtUfQ59yW/qX8l65ryzajCh2vKisegZwD+hNUhdarcSEM88rEdPORj8guAK8bjcSWJJPUnkn8yetceqXRFkOwpfml8kW7qfbqzi4VzMc4IiAbGPUkkD96imt94M7vi1Ajjx1ZQXJ9+pAHxzUNC1zVUtRN/A309k0VvL8T+P0O9zPJKxaV2dic5Y55+Pb6V0ArmlUPc9OMVFYRxXNK4odFFUkhVBYngADJJ9gPWu8MLu22NGdvZQWP6CrP7GdkEgVJ5lJnxnB6RkjBAA6nBxnn4qyutzZi1mthpo5e76L+9CLRd3t2QCXhXP4SxyPg+WlW5ilbfs8D57731Xqvkc0pSrjzBSlKAqjvRUf2roxxz4vX1+/D6/U/rVr0pQCuDSlAa3XnK28hUkEK2CDgjj3qjLW6kfW9PLuzHxWGWYk4yeMn060pUH5kaq/+CX7f5LG775CNJl2kjLxg4OMjd0PuPiqxsP8OP8A0L/QUpVGq5I9XsLzy9l/k9Fc0pWI+iFKUoBSlKAUpSgFKUoBSlKAUpSgFKUoBXVqUoETrukUbpjgZ2rz6+vrVl0pXo0eRHxvaf8A2ZHalKVcYD//2Q==";


            Base64String = ImageString6;

            
            Image img1 = ImageFromBase64String(ImageString3);
        }
        #endregion

        #region ------------- Properties, Indexer ---------------------------------
        /// <summary>
        /// Gets or sets the list with all Unit data.
        /// </summary>
        public ObservableCollection<Unit> Units { get; set; }

        /// <summary>
        /// Gets the Add units button command.
        /// </summary>
        public ICommand AddUnitCommand { get; private set; }

        /// <summary>
        /// Gets or sets the NewUnit from Textbox
        /// </summary>
        public string NewUnit { get; set; }

        public string Base64String { get; set; }
        #endregion

        #region ------------- Events ----------------------------------------------
        /// <summary>
        /// Event handler to notice changes in the current unit data.
        /// </summary>
        /// <param name="unit">Reference to the student data.</param>
        public void OnUnitDataChanged(Unit unit)
        {
            bool isNew = false;
            foreach (var item in Units)
            {
                if (unit.UnitName !=item.UnitName)
                {
                    isNew = true;
                }
            }
            if (isNew)
            {
                this.Units.Add(unit);
            }
            else
            {
                var unitToUpdate = this.Units.FirstOrDefault(s => s.UnitName == unit.UnitName);
                unitToUpdate.UnitName = unit.UnitName;  
            }
        }
        #endregion

        #region ------------- Private helper --------------------------------------
        /// <summary>
        /// Generate Unit data from db.
        /// </summary>
        private void LoadUnits()
        {
            // init collection and add data from db
            this.Units = new ObservableCollection<Unit>();

        }

        public string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, format);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }

        public Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();

            return result;
        }
        public string BuildImgString(string path)
        {

            Image img = Image.FromFile(path);
            string ImageString = ImageToBase64String(img, ImageFormat.Jpeg);
            return ImageString;
        }

        #endregion

        #region ------------- Commands --------------------------------------------
        /// <summary>
        /// Determines, whether the add amount command can be executed.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns><c>true</c> if the command can be executed, otherwise <c>false</c></returns>
        private bool AddUnitCommandCanExecute(object parameter)
        {
           

            return true;
        }

        /// <summary>
        /// Occurs, when the user clicks the "Add unit" button.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        private void AddUnitCommandExecute(object parameter)
        {
            Unit unit;
            unit = new Unit(this.NewUnit);
            Units.Add(unit);
            // publish event when unit is added
            EventAggregator.GetEvent<UnitDataChangedEvent>().Publish(unit);
        }
        #endregion
    }
    
}
