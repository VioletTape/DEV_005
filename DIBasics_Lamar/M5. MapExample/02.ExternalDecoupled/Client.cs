namespace DIBasics_Lamar.Core.ExternalDecoupled;

public class Client {
    public void Run() {
        var map = new Map();
        map.RouteType = RouteType.PublicTransport;


        map.Draw("from", "to");
    }
}

public class Map {
    readonly MapProvider mapProvider;

    public RouteType RouteType { get; set; }

    public Map() {
        mapProvider = new MapProvider();
        // настройка класса, который будем использовать 
        mapProvider.AddProvider(MapType.Here, new HereMaps());
        mapProvider.AddProvider(MapType.Yandex, new YandexMaps());
        mapProvider.AddProvider(MapType.Google, new GoogleMap());
    }

    public void Draw(string from, string to) {
        var route = mapProvider.GetPath(from, to, RouteType);

        // do some drawing
    }
}

public class MapProvider {
    // -- это у нас уходит на уровень выше, делеигруем настройку
    // readonly GoogleMap googleMap;
    // HereMaps hereMaps;
    // YandexMaps yandexMaps;
    readonly Dictionary<MapType, IRouteProvider> routeProviders;

    public MapType MapType { get; set; }

    public MapProvider() {
        routeProviders = new Dictionary<MapType, IRouteProvider>();
    }

    public MapProvider AddProvider(MapType mapType, IRouteProvider provider) {
        routeProviders.Add(mapType, provider);
        return this;
    }


    public Route GetPath(string from, string to, RouteType type) {
        // -- это тоже уходит и код становится сильно проще 
        // var route = new Route();

        // switch (MapType) {
        //     case this.MapType.Google:
        //         route = googleMap.GetPath(from, to, type);
        //         break;
        //     case this.MapType.Yandex:
        //         route = yandexMaps.GetPath(from, to, type); 
        //         break;
        //     case this.MapType.Here:
        //         route = hereMaps.GetPath(from, to, type);
        //         break;
        // }

        // return route;

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

// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
public interface IRouteProvider {
    Route GetPath(string from, string to, RouteType type);
}

public class GoogleMap : IRouteProvider {
    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class HereMaps : IRouteProvider {
    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class YandexMaps : IRouteProvider {
    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class Route { }
