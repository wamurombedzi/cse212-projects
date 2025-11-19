public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    // Represents the "features" array in the JSON
    public List<Feature> Features { get; set; }
}

public class Feature
{
    // Represents the "properties" object in each feature
    public Properties Properties { get; set; }
}

public class Properties
{
    // Represents the "mag" and "place" attributes
    public double? Mag { get; set; }
    public string Place { get; set; }
}

