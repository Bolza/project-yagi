struct Stat {
    public float baseValue;
    public float[] modifiers;
    public float current;

    public void init(float basev, float cur) {
        baseValue = basev;
        current = cur;
    }

    public float getCurrent() {
        return baseValue + modifiers[0];
    }

    //public float addModifier() {

    //}
}

