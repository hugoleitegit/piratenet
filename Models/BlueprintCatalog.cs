namespace Piratenet.Models;

public sealed record BlueprintDefinition(string Id, string Color, string SpanishName, string EnglishName);

public static class BlueprintCatalog
{
    public static readonly string[] GoldSystems = { "draco", "sol", "mizar", "gemini", "antares", "vega" };

    public static readonly string[] GoldDisplaySystems = { "draco", "sol", "mizar", "gemini", "antares", "vega" };

    public static readonly string[] AncientSystems = { "draco", "sol", "mizar", "gemini", "antares", "vega" };

    public static readonly string[] AncientVariants = { "ptt", "normal", "agil" };

    public static readonly string[] TauCetiVariants =
    {
        "symbiotic",
        "perceptonic",
        "multi_agent",
        "modal",
        "neural",
        "axiomatic",
        "thinking_body",
        "dendritic",
        "tau_ceti",
    };

    public static readonly string[] RemanenteVariants = { "ptt", "normal", "agil" };

    public static readonly IReadOnlyDictionary<string, string> SystemSpanishNames =
        new Dictionary<string, string>
        {
            ["draco"] = "Draconis",
            ["sol"] = "Sol",
            ["mizar"] = "Mizar",
            ["gemini"] = "Gemini",
            ["antares"] = "Antares",
            ["vega"] = "Vega",
        };

    public static readonly IReadOnlyDictionary<string, string> SystemEnglishNames =
        new Dictionary<string, string>
        {
            ["draco"] = "Draconis",
            ["sol"] = "Sol",
            ["mizar"] = "Mizar",
            ["gemini"] = "Gemini",
            ["antares"] = "Antares",
            ["vega"] = "Vega",
        };

    public static readonly BlueprintDefinition[] Plans =
    {
        new("blaster", "#ffffff", "Cañón", "Blaster"),
        new("collector", "#ffffff", "Recolector", "Collector"),
        new("repairer", "#ffffff", "Reparador", "Repairer"),
        new("booster", "#ffffff", "Impulsor", "Booster"),
        new("missiles", "#d98880", "Misiles", "Missiles"),
        new("computer_sight", "#d98880", "Mira computarizada", "Computer sight"),
        new("driller", "#d98880", "Perforador", "Driller"),
        new("thermal_ray", "#d98880", "Rayo Térmico", "Thermal ray"),
        new("shield", "#f9e79f", "Escudo", "Shield"),
        new("miner", "#f9e79f", "Picador", "Miner"),
        new("interference_signal", "#f9e79f", "Señal de interferencia", "Interference signal"),
        new("aggression_bomb", "#f9e79f", "Bomba de agresión", "Aggression bomb"),
        new("accelerator", "#85c1e9", "Acelerador", "Accelerator"),
        new("decelerator", "#85c1e9", "Desacelerador", "Decelerator"),
        new("decoy", "#85c1e9", "Señuelo", "Decoy"),
        new("paralyzer", "#85c1e9", "Paralizador", "Paralyzer"),
        new("protector", "#a9dfbf", "Protector", "Protector"),
        new("tele_repairer", "#a9dfbf", "Telerreparador", "Tele-repairer"),
        new("repair_field", "#a9dfbf", "Campo de reparación", "Repair field"),
        new("resurrector", "#a9dfbf", "Resurrector", "Resurrector"),
        new("long_range", "#eb984e", "Largo", "Long range"),
        new("attack_drone", "#eb984e", "Droide de ataque", "Attack drone"),
        new("orbital", "#eb984e", "Orbital", "Orbital"),
        new("attack_cargo", "#eb984e", "Carga de ataque", "Attack cargo"),
        new("attack_tower", "#bb8fce", "Torre de ataque", "Attack tower"),
        new("repair_tower", "#bb8fce", "Torre de reparación", "Repair tower"),
        new("mine", "#bb8fce", "Mina", "Mine"),
        new("lapa_bomb", "#bb8fce", "Bomba lapa", "Lapa bomb"),
        new("absorber", "#a6b0b7", "Absorbedor", "Absorber"),
        new("inverter", "#a6b0b7", "Inversor", "Inverter"),
        new("trap", "#a6b0b7", "Trampa", "Trap"),
        new("jump", "#a6b0b7", "Salto", "Jump"),
        new("cloud", "#a6b0b7", "Nube", "Cloud"),
        new("deflector", "#a6b0b7", "Deflector", "Deflector"),
        new("aura", "#a6b0b7", "Aura", "Aura"),
        new("electric_beam", "#a6b0b7", "Haz eléctrico", "Electric beam"),
    };

    private static readonly IReadOnlyDictionary<string, BlueprintDefinition> ById =
        Plans.ToDictionary(plan => plan.Id, StringComparer.Ordinal);

    public static BlueprintDefinition GetPlan(string planId) => ById[planId];

    public static string GetPlanName(string planId, string language) =>
        language.Equals("en", StringComparison.OrdinalIgnoreCase)
            ? ById[planId].EnglishName
            : ById[planId].SpanishName;

    public static string GetSystemName(string systemId, string language) =>
        (language.Equals("en", StringComparison.OrdinalIgnoreCase) ? SystemEnglishNames : SystemSpanishNames)[systemId];

    public static string GoldKey(string systemId, string planId) => $"{systemId}.{planId}";

    public static string AncientKey(string systemId, string planId, string variant) =>
        $"ancient.{systemId}.{planId}.{variant}";

    public static string TauCetiKey(string planId, string variant) =>
        $"tauceti.{planId}.{variant}";

    public static string RemanenteKey(string planId, string variant) =>
        $"remanente.{planId}.{variant}";
}
