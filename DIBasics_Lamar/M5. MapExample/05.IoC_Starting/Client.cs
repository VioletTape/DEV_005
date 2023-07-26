using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace DIBasics_Lamar.Core.IoC2;

public class Client {
    public void Run() {
        /*
         * Теперь у нас все провайдеры регистрируются автоматически,
         * исключается ситуация, когда забыли добавить регистрацию
         * и падение идет в рантайме. 
         */
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

        map.RouteType = RouteType.PublicTransport;
        map.Draw("from", "to");


        // var providers = new IRouteProvider[3] {
        //                                           new HereMaps(), new HereMaps(), new YandexMaps()
        //                                       };
        //
        // var mapProvider = new MapProvider(providers);
        // var map = new Map(mapProvider);
        // map.RouteType = RouteType.PublicTransport;


        map.Draw("from", "to");
    }
}

public class Map {
    readonly MapProvider mapProvider;

    public RouteType RouteType { get; set; }

    public Map(MapProvider mapProvider) {
        this.mapProvider = mapProvider;
    }

    public void Draw(string from, string to) {
        var route = mapProvider.GetPath(from, to, RouteType);

        // do some drawing
    }
}

public class MapProvider {
    readonly Dictionary<MapType, IRouteProvider> routeProviders = new();

    public MapType MapType { get; set; }

    public MapProvider(IRouteProvider[] providers) {
        foreach (var r in providers) {
                routeProviders.Add(r.Type, r);
        }
    }

    public Route GetPath(string from, string to, RouteType type) {
        return routeProviders[MapType].GetPath(from, to, type);
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
