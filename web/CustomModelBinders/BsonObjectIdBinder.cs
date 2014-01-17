namespace FirstStep.CustomModelBinders
{
    #region Using

    using System.Web.Mvc;
    using MongoDB.Bson;

    #endregion
    
    /// <summary>
    /// Converts from string to bsonobjectid for the view model instances
    /// </summary>
    public class BsonObjectIdBinder : IModelBinder
    {
        public object BindModel(ControllerContext ctx, ModelBindingContext mctx)
        {
            //Retrieve a value object using modelBindingContext.ModelName as the key            
            var valueProviderResult = mctx.ValueProvider.GetValue(mctx.ModelName);

            //Now, create and return a new instance of MongoDB.Bson.ObjectId with the raw string retrieved from the model's property            
            return new ObjectId(valueProviderResult.AttemptedValue);
        }// BindModel(...)

    }// class
}// namespace