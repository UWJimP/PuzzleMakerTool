using UnityEngine;

public class CodeGenerator {

    private static readonly char[] CHARS = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789".ToCharArray();

    public static string GenerateCode(int length) {
        if (length > 0) {
            string code = "";
            for (int count = 0; count < length; count++) {
                code += CHARS[Random.Range(0, CHARS.Length)];
            }
            return code;
        } else {
            return null;
        }
    }
}
