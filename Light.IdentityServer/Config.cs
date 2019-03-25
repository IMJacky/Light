using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Light.IdentityServer
{
    public class Config
    {
        // clients that are allowed to access resources from the Auth server 
        public static IEnumerable<Client> GetClients()
        {
            // client credentials, list of clients
            return new List<Client>
            {
                new Client
                {
                    ClientId = "WebAppClientId",
                    AllowedGrantTypes = { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword },
                    ClientSecrets =
                    {
                        new Secret("WebAppClientSecret".Sha256())
                    },
                    //AllowedScopes = { "AuthApi" }
                },
            };
        }

        // API that are allowed to access the Auth server
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("AuthApi", "Auth API-权限接口"),
                new ApiResource("BlogApi", "Blog API-博客接口")
            };
        }
    }
}
