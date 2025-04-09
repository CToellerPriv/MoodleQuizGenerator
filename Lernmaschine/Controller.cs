
namespace Lernmaschine
{
    internal class Controller : IController
    {
        private IModel model;
        private IView view;

        IModel IController.Model { set => model=value; }
        IView IController.View { set => view=value; }

        void IController.aendern(Karteikarte karteikarte)
        {
            model.aendern(karteikarte);
        }

        void IController.einfuegen(Karteikarte karteikarte)
        {
            model.einfuegen(karteikarte);
        }

        void IController.loeschen(Karteikarte karteikarte)
        {
            model.loeschen(karteikarte);
        }

        void IController.oeffnen(string pfad)
        {
            model.oeffnen (pfad);
        }

        List<Karteikarte> IController.suchen(Karteikarte karteikarte)
        {
            throw new NotImplementedException();
        }
    }
}