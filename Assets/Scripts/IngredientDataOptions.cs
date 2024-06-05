//This script is used storing the possiblities for ingredient types and effects

using System;

[Serializable]
public static class IngredientDataOptions
{
    public enum EffectType { Damage, Healing, ForLoop, Multiply, Nothing, Return }
    public enum IngredientType { Potion, Code }
}
