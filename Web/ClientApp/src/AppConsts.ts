export class AppConsts {

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish
    static baseUrl = "http://localhost:5000"; 
      static baseUrlImage = "http://localhost:5001/"; 
    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'YouthAndSports'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };
    static readonly maps = {
        OpenStreetMapService: 'https://nominatim.openstreetmap.org/reverse?key=iTzWSiYpGxDvhATNtSrqx5gDcnMOkntL&format=json&addressdetails=1&lat={lat}&lon={lon}&accept-language=ar',
        OpenStreetMapAutoCompleteService: 'https://nominatim.openstreetmap.org/search?format=json&accept-language=ar&q=',
    };
}
