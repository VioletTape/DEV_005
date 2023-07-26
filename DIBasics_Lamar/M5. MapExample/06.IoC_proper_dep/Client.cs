using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace DIBasics_Lamar.Core.IoC3;

public class Client {
    public void Run() {
        var container = new Container(x => {
                          x.Scan(s => {
                                     s.TheCallingAssembly();
                                     s.WithDefaultConventions();
                                     s.AddAllTypesOf<IRouteProvider>();
                                 }
                                );
                          x.For<MapProvider>().Use<MapProvider>();
                          x.For<Map>().Use<Map>();
                      }
                     );

        container.AssertConfigurationIsValid();

        var map = container.GetRequiredService<Map>();

        /*
         * Теперь стратегия тоже как класс, а не свойство, которое можно
         * забыть выставить корректно. Вся информация для отрисовки маршрута
         * передается непосредственно в метод. 
         */
        map.Draw(new PublicTransportStrategy {
                                                 From = "from", 
                                                 To = "to"
                                                 , Type = MapType.Google
                                             });
    }
}

public class Map {
    readonly MapProvider mapProvider;

    public Map(MapProvider mapProvider) {
        this.mapProvider = mapProvider;
    }

    public void Draw(RouteStrategy strategy) {
        var route = mapProvider.GetPath(strategy);

        // do some drawing
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
