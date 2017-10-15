namespace KiraNet.GutsMvc.ModelValid
{
    public class ModelValid
    {
        public ModelValid()
        {
        }

        public ModelValid(ModelValid modelValid)
        {
            if(modelValid.ValidResults!=null)
            {
                ValidResults = modelValid.ValidResults;
            }
        }
        public ValidFailureCollection ValidResults { get; } = new ValidFailureCollection();
    }
}
