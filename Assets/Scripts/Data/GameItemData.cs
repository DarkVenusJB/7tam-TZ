namespace Scripts.Data
{
    public class GameItemData
    {
        public EFigureType FigureType { get; }
        public EAnimalType AnimalType { get; }
        public EColorType ColorType { get; }

        public GameItemData(EFigureType figureType, EAnimalType animalType, EColorType colorType)
        {
            FigureType = figureType;
            AnimalType = animalType;
            ColorType = colorType;
        }
    }
}