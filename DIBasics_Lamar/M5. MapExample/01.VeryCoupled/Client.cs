namespace DIBasics_Lamar.Core.VeryCoupled;

public class Client {
    public void Run() {
        /*
         * На первый взгляд все красиво и хорошо, вроде со стороны
         * клиента только карта и нужна. Все задается параметрами
         * и вызывается отрисовка от точки до точки. 
         */
        var map = new Map();
        map.RouteType = RouteType.PublicTransport;

        map.Draw("from", "to");
    }
}

/*
 * Далее очень много внутренних зависимостей и оператора new для
 * создания классов. Старайтесь минимизировать использование new
 * в своих приложениях. 
 */
public class Map {
    MapProvider mapProvider;

    public RouteType RouteType { get; set; }

    public Map() {
        mapProvider = new MapProvider();
    }

    public void Draw(string from, string to) {
        var route = mapProvider.GetPath(from, to, RouteType);

        // do some drawing
    }
}

public class MapProvider {
    readonly GoogleMap googleMap;
    HereMaps hereMaps;
    YandexMaps yandexMaps;

    public MapType MapType { get; set; }

    public MapProvider() {
        googleMap = new GoogleMap();
        hereMaps = new HereMaps();
        yandexMaps = new YandexMaps();
    }


    public Route GetPath(string from, string to, RouteType type) {
        var route = new Route();

        switch (MapType) {
            case MapType.Google:
                route = googleMap.GetPath(from, to, type);
                break;
            case MapType.Yandex:
                route = yandexMaps.GetPath(from, to, type); 
                break;
            case MapType.Here:
                route = hereMaps.GetPath(from, to, type);
                break;
        }

        return route;
    }

}

public enum MapType {
    Google
  , Here
  , Yandex
}

public enum RouteType {
    PublicTransport
  , Car
  , ByFoot
}

public class GoogleMap {
    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class HereMaps {
    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}

public class YandexMaps {
    public Route GetPath(string from, string to, RouteType type) {
        // real call to external service
        return new Route();
    }
}


public class Route{}