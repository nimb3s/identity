// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Nimb3s.Identity.Samples.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Profile(),
                //new IdentityResource("api1", "api 1", new List<string> { new IdentityResources.Email().Name })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "api 1"),
            };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
            {
                new ApiResource
                { 
                    Name = "GaiaApi",
                    DisplayName = "Gaia REST API",
                    Scopes = new List<string>
                    {
                        "https://nimb3s.com/gaia",
                        "api1"
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "Nimb3sSPA",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("1234".Sha256())
                    },
                    AllowedGrantTypes = new List<string>
                    { 
                        GrantType.ClientCredentials,

                    },
                    AllowedScopes = new List<string>
                    {
                        //new IdentityResources.OpenId().Name
                        "api1",
                        "https://nimb3s.com/gaia"
                    }
                }
            };
    }
}