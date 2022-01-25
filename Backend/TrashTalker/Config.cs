using System;
using System.Collections.Generic;

namespace TrashTalker
{
    public static class Config
    {
        //JsonWebToken
        public const string SECRET = "DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4";
        public const int TOKEN_DURATION = 30;

        //GOOGLE
        public const string GOOGLE_API_URL = "https://maps.googleapis.com/maps/api/distancematrix/json";
        public const string GOOGLE_API_KEY = "<<GOOGLE KEY>>";

        public const string LAT_LONG_DELIMITATOR = "%2C";
        public const string CORDINATOR_DELIMITATOR = "%7C";

        //ARDUINO
        public const string ARDUINO_GRANT_TYPE = "client_credentials";
        public const string ARDUINO_CLIENT_ID = "2aeUptMIGl6tViCt1CE0uzQuHIKywuKB";
        public const string ARDUINO_CLIENT_SECRET = "uYuBiIiRETWHaZpwvfCf7dhd4DIZM78UHq1pBaNPwKLzK2j3BBzYcJ5XDKY2PDQi";
        public const string URL_ARDUINO_AUDIENCE = "https://api2.arduino.cc/iot";
        public static readonly string URL_ARDUINO_TOKEN = $"{URL_ARDUINO_AUDIENCE}/v1/clients/token";


        // public static (string lati, string longi) startingPoint = ("41.36748033438808", "-8.194979043944143");

        public static IDictionary<string, string> startingPoint = new Dictionary<string, string>()
        {
            {"latitude", "41.36748033438808"},
            {"longitude", "-8.194979043944143"}
        };

        //LOCALIZACAO EMPRESA -> ESTG
        public static readonly string COMPANY_LOCATION_GOOGLE_API =
            $"{startingPoint["latitude"]}{LAT_LONG_DELIMITATOR}{startingPoint["longitude"]}";

        //GEOLOCATIONAPI
        public const string GEOLOCATION_API = "https://api.duminio.com/ptcp/v2/ptapi619feccf1a9a56.85917744/";

        //7 HOURS
        public static readonly TimeSpan MAX_DURATION_ROUTE = new(0, 4, 0, 0);

        //CAPACITY FOR ALERT CONTAINER ALERT
        public const double MAX_ALERT_CAPACITY = .8;
        public const double SCALED_PERCENTAGE_AUTOMATIC_ROUTE = .2;

        //CONTAINER DIMENSIONS
        public const int HEIGHT = 300;
        public const int WIDTH = 300;
        public const int DEPTH = 300;
    }
}