﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShoppingApp.Domain.Entities;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence;

namespace ShoppingApp.Infrastructure.Services
{
    public static class SeedData
    {
        public static async Task SeedRolesAndSuperAdminAsync(
            UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager,
            ILogger logger)
        {
            try
            {
                // Define all roles
                string[] roleNames = { "SuperAdmin", "Admin", "AppUser", "Seller" };

                // Create roles if they don't exist
                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        var role = new AppRole
                        {
                            Id = Ulid.NewUlid(),
                            Name = roleName
                        };
                        var result = await roleManager.CreateAsync(role);
                        
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"Role '{roleName}' created successfully.");
                        }
                        else
                        {
                            logger.LogError($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }

                // Create super admin user
                const string superAdminEmail = "madeymohey1@gmail.com";
                const string superAdminPassword = "Madey.mohey811";
                
                var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);
                
                if (superAdmin == null)
                {
                    superAdmin = new AppUser
                    {
                        Id = Ulid.NewUlid(),
                        UserName = "SuperAdmin",
                        Email = superAdminEmail,
                        EmailConfirmed = true,
                        FullName = "Super Admin"
                    };

                    var createResult = await userManager.CreateAsync(superAdmin, superAdminPassword);

                    if (createResult.Succeeded)
                    {
                        logger.LogInformation($"Super admin user '{superAdminEmail}' created successfully.");

                        // Create Cart and Wishlist for the super admin
                        // Note: This requires ApplicationDbContext, so we'll handle this separately
                        
                        // Assign all roles to super admin
                        foreach (var roleName in roleNames)
                        {
                            var addToRoleResult = await userManager.AddToRoleAsync(superAdmin, roleName);
                            if (addToRoleResult.Succeeded)
                            {
                                logger.LogInformation($"Super admin assigned to role '{roleName}'.");
                            }
                            else
                            {
                                logger.LogError($"Failed to assign super admin to role '{roleName}': {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                            }
                        }
                    }
                    else
                    {
                        logger.LogError($"Failed to create super admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    logger.LogInformation($"Super admin user '{superAdminEmail}' already exists.");
                    
                    // Ensure super admin has all roles
                    foreach (var roleName in roleNames)
                    {
                        if (!await userManager.IsInRoleAsync(superAdmin, roleName))
                        {
                            var addToRoleResult = await userManager.AddToRoleAsync(superAdmin, roleName);
                            if (addToRoleResult.Succeeded)
                            {
                                logger.LogInformation($"Super admin assigned to role '{roleName}'.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding roles and super admin.");
                throw;
            }
        }

        public static async Task SeedCartAndWishlistForSuperAdminAsync(
            ApplicationDbContext db,
            UserManager<AppUser> userManager,
            ILogger logger)
        {
            try
            {
                const string superAdminEmail = "madeymohey1@gmail.com";
                var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

                if (superAdmin != null)
                {
                    // Check if cart exists
                    var existingCart = db.Carts.FirstOrDefault(c => c.UserId == superAdmin.Id);
                    if (existingCart == null)
                    {
                        var cart = new Cart
                        {
                            Id = Ulid.NewUlid(),
                            UserId = superAdmin.Id
                        };
                        await db.Carts.AddAsync(cart);
                        logger.LogInformation("Cart created for super admin.");
                    }

                    // Check if wishlist exists
                    var existingWishlist = db.Wishlists.FirstOrDefault(w => w.UserId == superAdmin.Id);
                    if (existingWishlist == null)
                    {
                        var wishlist = new Wishlist
                        {
                            Id = Ulid.NewUlid(),
                            UserId = superAdmin.Id
                        };
                        await db.Wishlists.AddAsync(wishlist);
                        logger.LogInformation("Wishlist created for super admin.");
                    }

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding cart and wishlist for super admin.");
                throw;
            }
        }
    }
}
