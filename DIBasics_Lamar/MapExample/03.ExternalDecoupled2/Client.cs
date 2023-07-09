namespace DIBasics_Lamar.Core.ExternalDecoupled2;

public class Client {
    public void Run() {
        // переносим настройку еще выше, теперь по факту корень 
        // только знает как настраиваются классы далее 
        var mapProvider = new MapProvider();
        mapProvider.AddProvider(MapType.Here, new HereMaps());
        mapProvider.AddProvider(MapType.Yandex, new YandexMaps());
        mapProvider.AddProvider(MapType.Google, new GoogleMap());


        var map = new Map(mapProvider);
        map.RouteType = RouteType.PublicTransport;


        map.Draw("from", "to");
    }
}

public class Map {
    readonly MapProvider mapProvider;

    public RouteType RouteType { get; set; }

    // передаем зависимости извне, карта не знает о провайдерах 
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

    public MapProvider AddProvider(MapType mapType, IRouteProvider provider) {
        routeProviders.Add(mapType, provider);
        return this;
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
