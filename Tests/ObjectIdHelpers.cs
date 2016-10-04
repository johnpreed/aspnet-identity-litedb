using LiteDB;

namespace Tests
{
    public static class ObjectIdHelpers
    {
        public static ObjectId SafeParseObjectId(this string id)
        {
            try
            {
                return new ObjectId(id);
            }
            catch
            {
                return null;
            }
        }
    }
}