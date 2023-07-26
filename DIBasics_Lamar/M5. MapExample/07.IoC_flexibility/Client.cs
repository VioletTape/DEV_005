using Lamar;
using Lamar.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DIBasics_Lamar.Core.IoC4;

public class Client {
    private Container container;

    public void Startup() {
        container = new Container(x => {
                                      x.Scan(s => {
                                                 s.TheCallingAssembly();
                                                 s.WithDefaultConventions();
                                                 s.AddAllTypesOf<IRouteProvider>();
                                             }
                                            );
                                      // новая зависимость появилась, но изменения очень точечные
                                      x.For<ICanvas>().Use<Canvas>();
                                      x.For<MapProvider>().Use<MapProvider>();
                                      x.For<Map>().Use<Map>();
                                  }
                                 );
    }

    public void Run() {
        var map = container.GetRequiredService<Map>();

        map.Draw(new PublicTransportStrategy {
                                                 From = "from", 
                                                 To = "to"
                                                 , Type = MapType.Google
                                             });
    }
}

public class Map {
    readonly MapProvider mapProvider;
    readonly ICanvas canvas;

    /*
     * Новый параметр, который говорит где будем рисовать карту 
     */
    public Map(MapProvider mapProvider, ICanvas canvas) {
        this.mapProvider = mapProvider;
        this.canvas = canvas;
    }

    public void Draw(RouteStrategy strategy) {
        var route = mapProvider.GetPath(strategy);

        canvas.Process(route);
        // do some drawing
    }
}

public interface ICanvas {
    void Process(Route route);
}

public class Canvas : ICanvas {
    public void Process(Route route) {
        
    }
}

public class MapProvider {
    readonly Dictionary<MapType, IRouteProvider> routeProviders = new();

    public MapProvider(IRouteProvider[] providers) {
        foreach (var r in providers) {
                routeProviders.Add(r.Type, r);
        }
    }

    public Route GetPath(RouteStrategy strategy) {
        return routeProviders[strategy.Type].GetPath(strategy.From, strategy.To, strategy.RouteType);
    }
}

public enum MapType {
    None
  , Google
  , Here
  , Yandex
}

public enum RouteType {
    PublicTransport
  , Car
  , ByFoot
}

public abstract class RouteStrategy {
    public readonly RouteType RouteType;
    public MapType Type { get; set; }
    public string From { get; set; }

    public string To { get; set; }

    protected RouteStrategy(RouteType routeType) {
        RouteType = routeType;
    }
}

public class PublicTransportStrategy : RouteStrategy {
    public PublicTransportStrategy() : base(RouteType.PublicTransport) { }
}

public class CarStrategy : RouteStrategy {
    public CarStrategy() : base(RouteType.Car) { }
}

public class ByFootStrategy : RouteStrategy {
    public ByFootStrategy() : base(RouteType.ByFoot) { }
}


public interface IRouteProvider {
    MapType Type { get; }
    Route GetPath(string from, string to, RouteType type);
}

public class GoogleMap : IRouteProvider {
    public MapType Type => MapType.Google;

    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class HereMaps : IRouteProvider {
    public MapType Type => MapType.Here;

    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class YandexMaps : IRouteProvider {
    public MapType Type => MapType.Yandex;

    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class Route { }
