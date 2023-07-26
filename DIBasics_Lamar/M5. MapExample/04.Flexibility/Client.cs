namespace DIBasics_Lamar.Core.Flexibility;

public class Client {
    public void Run() {
        var providers = new IRouteProvider[3] {
                                  new HereMaps(), new HereMaps(), new YandexMaps()
                              };

        /*
         * Еше более упрощаем код и делаем зависимости явными через
         * конструктор. До этого можно было "забыть" о провайдерах. 
         */
        var mapProvider = new MapProvider(providers);
        // mapProvider.AddProvider(MapType.Here, new HereMaps());
        // mapProvider.AddProvider(MapType.Yandex, new YandexMaps());
        // mapProvider.AddProvider(MapType.Google, new GoogleMap());


        var map = new Map(mapProvider);
        map.RouteType = RouteType.PublicTransport;


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

    public MapProvider(params IRouteProvider[] providers) {
        foreach (var r in providers) {
                routeProviders.Add(r.Type, r);
        }
    }

    // - это уходит. Еще меньше кода становится где можно сделать ошибку. 
    // public MapProvider AddProvider(MapType mapType, IRouteProvider provider) {
    //     routeProviders.Add(mapType, provider);
    //     return this;
    // }


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
    // Каждый провайдер обязан сообщить свой ключ для дальнейшего 
    // использования приложением 
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
