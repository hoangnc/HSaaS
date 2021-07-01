using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using MasterData.Permissions;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace HSaaS.IdentityServer
{
    public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private const string CommonSecret = "E5Xd4yMqjP5kjWFKrYgySBju6JVfCzMyFp7n2QmMrME=";

        private readonly string[] CommonScopes = new[]
        {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address",
                "HSaaS"
        };
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTenant _currentTenant;

        public IdentityServerDataSeedContributor(
            IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IApiScopeRepository apiScopeRepository,
            IIdentityResourceDataSeeder identityResourceDataSeeder,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder,
            IConfiguration configuration,
            ICurrentTenant currentTenant)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
            _configuration = configuration;
            _currentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                await _identityResourceDataSeeder.CreateStandardResourcesAsync();
                await CreateApiResourcesAsync();
                await CreateApiScopesAsync();
                await CreateClientsAsync();
            }
        }

        private async Task CreateApiScopesAsync()
        {
            await CreateApiScopeAsync("HSaaS");
            await CreateApiScopeAsync("IdentityService");
            await CreateApiScopeAsync("TenantManagementService");
            await CreateApiScopeAsync("SettingManagementService");
            await CreateApiScopeAsync("BloggingService");
            await CreateApiScopeAsync("MasterDataService");
            await CreateApiScopeAsync("DocumentManagementService");
            await CreateApiScopeAsync("InternalGateway");
            await CreateApiScopeAsync("BackendAdminAppGateway");
            await CreateApiScopeAsync("PublishWebSiteGateway");
        }

        private async Task CreateApiResourcesAsync()
        {
            var commonApiUserClaims = new[]
            {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };

            await CreateApiResourceAsync("HSaaS", commonApiUserClaims);
            await CreateApiResourceAsync("IdentityService", commonApiUserClaims);
            await CreateApiResourceAsync("TenantManagementService", commonApiUserClaims);
            await CreateApiResourceAsync("BloggingService", commonApiUserClaims);
            await CreateApiResourceAsync("MasterDataService", commonApiUserClaims);
            await CreateApiResourceAsync("DocumentManagementService", commonApiUserClaims);
            await CreateApiResourceAsync("InternalGateway", commonApiUserClaims);
            await CreateApiResourceAsync("BackendAdminAppGateway", commonApiUserClaims);
            await CreateApiResourceAsync("PublicWebSiteGateway", commonApiUserClaims);
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                    new ApiResource(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

        private async Task<ApiScope> CreateApiScopeAsync(string name)
        {
            var apiScope = await _apiScopeRepository.GetByNameAsync(name);
            if (apiScope == null)
            {
                apiScope = await _apiScopeRepository.InsertAsync(
                    new ApiScope(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            return apiScope;
        }

        private async Task CreateBackendAdminAppClient(IConfigurationSection configurationSection)
        {
            var clientId = configurationSection["HSaaS_Backend_Admin_App_Client:ClientId"];
            if (!clientId.IsNullOrWhiteSpace())
            {
                var clientRootUrl = configurationSection["HSaaS_Backend_Admin_App_Client:RootUrl"].EnsureEndsWith('/');
                await CreateClientAsync(
                   clientId,
                   CommonScopes.Union(new[] { "BackendAdminAppGateway", "IdentityService", "InternalGateway", "MasterDataService", "DocumentManagementService", "TenantManagementService" }),
                   new[] { "hybrid" },
                   CommonSecret,
                   permissions: new[] { IdentityPermissions.Users.Default, "DocumentManagement.Documents" },
                   redirectUri: $"{clientRootUrl}/signin-oidc",
                   postLogoutRedirectUri: $"{clientRootUrl}/signout-callback-oidc",
                   frontChannelLogoutUri: $"{clientRootUrl}Account/FrontChannelLogout"
                );
            }
        }
        private async Task CreatePublishWebsiteClient(IConfigurationSection configurationSection)
        {
            var clientId = configurationSection["HSaaS_Public_Website_Client:ClientId"];
            if (!clientId.IsNullOrWhiteSpace())
            {
                var clientRootUrl = configurationSection["HSaaS_Public_Website_Client:RootUrl"].EnsureEndsWith('/');
                await CreateClientAsync(
                    clientId,
                    CommonScopes.Union(new[] { "PublicWebSiteGateway", "InternalGateWay", "MasterDataService", "DocumentManagementService" }),
                    new[] { "hybrid" },
                    CommonSecret,
                    permissions: new[] { "DocumentManagement.Documents",
                        MasterDataPermissions.Companies.Default,
                        MasterDataPermissions.Departments.Default,
                        MasterDataPermissions.DocumentTypes.Default,
                        MasterDataPermissions.Modules.Default },
                   redirectUri: $"{clientRootUrl}/signin-oidc",
                   postLogoutRedirectUri: $"{clientRootUrl}/signout-callback-oidc",
                   frontChannelLogoutUri: $"{clientRootUrl}Account/FrontChannelLogout"
                );
            }
        }

        private async Task CreateClientsAsync()
        {
            var configurationSection = _configuration.GetSection("IdentityServer:Clients");

            //Web Client
            var webClientId = configurationSection["HSaaS_Web:ClientId"];
            if (!webClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["HSaaS_Web:RootUrl"].EnsureEndsWith('/');

                /* HSaaS_Web client is only needed if you created a tiered
                 * solution. Otherwise, you can delete this client. */

                await CreateClientAsync(
                    name: webClientId,
                    scopes: CommonScopes,
                    grantTypes: new[] { "hybrid" },
                    secret: (configurationSection["HSaaS_Web:ClientSecret"] ?? "1q2w3e*").Sha256(),
                    redirectUri: $"{webClientRootUrl}signin-oidc",
                    postLogoutRedirectUri: $"{webClientRootUrl}signout-callback-oidc",
                    frontChannelLogoutUri: $"{webClientRootUrl}Account/FrontChannelLogout",
                    corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") }
                );
            }

            await CreateBackendAdminAppClient(configurationSection);
            await CreatePublishWebsiteClient(configurationSection);

            /*await CreateClientAsync(
               "hsaas-backend-admin-app-client",
               CommonScopes.Union(new[] { "BackendAdminAppGateway", "IdentityService", "InternalGateway", "MasterDataService", "DocumentManagementService", "TenantManagementService" }),
               new[] { "hybrid" },
               CommonSecret,
               permissions: new[] { IdentityPermissions.Users.Default, "DocumentManagement.Documents" },
               redirectUri: "https://localhost:44354/signin-oidc",
               postLogoutRedirectUri: "https://localhost:44354/signout-callback-oidc"
           );

            await CreateClientAsync(
                "hsaas-public-website-client",
                CommonScopes.Union(new[] { "PublicWebSiteGateway", "InternalGateWay", "MasterDataService", "DocumentManagementService" }),
                new[] { "hybrid" },
                CommonSecret,
                permissions: new[] { "DocumentManagement.Documents",
                    MasterDataPermissions.Companies.Default,
                    MasterDataPermissions.Departments.Default,
                    MasterDataPermissions.DocumentTypes.Default,
                    MasterDataPermissions.Modules.Default },
                redirectUri: "https://localhost:44335/signin-oidc",
                postLogoutRedirectUri: "https://localhost:44335/signout-callback-oidc"
            );*/

            await CreateClientAsync(
                "blogging-service-client",
                new[] { "InternalGateway", "IdentityService" },
                new[] { "client_credentials" },
                CommonSecret,
                permissions: new[] { IdentityPermissions.UserLookup.Default }
            );

            await CreateClientAsync(
                "master-data-service-client",
                new[] { "InternalGateway", "IdentityService" },
                new[] { "client_credentials" },
                CommonSecret
            );

            await CreateClientAsync(
                "document-management-service-client",
                new[] { "InternalGateway", "IdentityService" },
                new[] { "client_credentials" },
                CommonSecret,
                permissions: new[] {
                    MasterDataPermissions.Companies.Default,
                    MasterDataPermissions.Departments.Default,
                    MasterDataPermissions.DocumentTypes.Default,
                    MasterDataPermissions.Modules.Default
                }
            );

            if (!webClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["HSaaS_Web:RootUrl"].EnsureEndsWith('/');

                /* HSaaS_Web client is only needed if you created a tiered
                 * solution. Otherwise, you can delete this client. */

                await CreateClientAsync(
                    name: webClientId,
                    scopes: CommonScopes,
                    grantTypes: new[] { "hybrid" },
                    secret: (configurationSection["HSaaS_Web:ClientSecret"] ?? "1q2w3e*").Sha256(),
                    redirectUri: $"{webClientRootUrl}signin-oidc",
                    postLogoutRedirectUri: $"{webClientRootUrl}signout-callback-oidc",
                    frontChannelLogoutUri: $"{webClientRootUrl}Account/FrontChannelLogout",
                    corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") }
                );
            }

            //Console Test / Angular Client
            var consoleAndAngularClientId = configurationSection["HSaaS_App:ClientId"];
            if (!consoleAndAngularClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["HSaaS_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(
                    name: consoleAndAngularClientId,
                    scopes: CommonScopes,
                    grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                    secret: (configurationSection["HSaaS_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                    requireClientSecret: false,
                    redirectUri: webClientRootUrl,
                    postLogoutRedirectUri: webClientRootUrl,
                    corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") }
                );
            }

            // Blazor Client
            var blazorClientId = configurationSection["HSaaS_Blazor:ClientId"];
            if (!blazorClientId.IsNullOrWhiteSpace())
            {
                var blazorRootUrl = configurationSection["HSaaS_Blazor:RootUrl"].TrimEnd('/');

                await CreateClientAsync(
                    name: blazorClientId,
                    scopes: CommonScopes,
                    grantTypes: new[] { "authorization_code" },
                    secret: configurationSection["HSaaS_Blazor:ClientSecret"]?.Sha256(),
                    requireClientSecret: false,
                    redirectUri: $"{blazorRootUrl}/authentication/login-callback",
                    postLogoutRedirectUri: $"{blazorRootUrl}/authentication/logout-callback",
                    corsOrigins: new[] { blazorRootUrl.RemovePostFix("/") }
                );
            }

            // Swagger Client
            var swaggerClientId = configurationSection["HSaaS_Swagger:ClientId"];
            if (!swaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["HSaaS_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(
                    name: swaggerClientId,
                    scopes: CommonScopes,
                    grantTypes: new[] { "authorization_code" },
                    secret: configurationSection["HSaaS_Swagger:ClientSecret"]?.Sha256(),
                    requireClientSecret: false,
                    redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                    corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") }
                );
            }
        }

        private async Task<Client> CreateClientAsync(
            string name,
            IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret = null,
            string redirectUri = null,
            string postLogoutRedirectUri = null,
            string frontChannelLogoutUri = null,
            bool requireClientSecret = true,
            bool requirePkce = false,
            IEnumerable<string> permissions = null,
            IEnumerable<string> corsOrigins = null)
        {
            var client = await _clientRepository.FindByClientIdAsync(name);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false,
                        FrontChannelLogoutUri = frontChannelLogoutUri,
                        RequireClientSecret = requireClientSecret,
                        RequirePkce = requirePkce
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (!secret.IsNullOrEmpty())
            {
                if (client.FindSecret(secret) == null)
                {
                    client.AddSecret(secret);
                }
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions,
                    null
                );
            }

            if (corsOrigins != null)
            {
                foreach (var origin in corsOrigins)
                {
                    if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                    {
                        client.AddCorsOrigin(origin);
                    }
                }
            }

            return await _clientRepository.UpdateAsync(client);
        }
    }
}
