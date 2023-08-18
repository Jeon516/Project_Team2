// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("dPf59sZ09/z0dPf39mzVZzwXgdo7SVp3KdUuG/yWqJFnpywwDOHqxMZ099TG+/D/3HC+cAH79/f38/b1ENiMRZHhnUtdfxgEj8bnHg5OtL2+b80UQZjtZlBEho86Yqe6CJM2fLvYfdnqCLus1HFhDNvIolrKq5nH9/K+vOOLN5B2A3uZh06WJGy515deYMWzL9wHRsTRnSC7QUS76rHWqA/2sWGPARrjofVYAV+yx4L/1bQHh6UnclXWZtzUIQdgdtQpW2uJLMzNFzdHXj5PmnwvhGJLqB+hdQQ1/Z+Wv8EoSwwhm7gsp73/+tHqOOOiYna3JPYCzpLd9Rxetc2wZ7N4aVYNqpz0P+6D9I0dViDGFKlTxIEHDHi15qFnLsYuh/T19/b3");
        private static int[] order = new int[] { 1,3,3,4,13,13,9,11,10,10,12,12,12,13,14 };
        private static int key = 246;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
