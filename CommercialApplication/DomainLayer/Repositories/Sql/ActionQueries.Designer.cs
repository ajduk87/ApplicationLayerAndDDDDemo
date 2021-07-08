﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommercialApplication.DomainLayer.Repositories.Sql {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ActionQueries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ActionQueries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CommercialApplication.DomainLayer.Repositories.Sql.ActionQueries", typeof(ActionQueries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM commercialapplication.action 
        ///WHERE id = @id.
        /// </summary>
        internal static string Delete {
            get {
                return ResourceManager.GetString("Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT EXISTS (SELECT 1 FROM commercialapplication.action WHERE id  =@id).
        /// </summary>
        internal static string Exists {
            get {
                return ResourceManager.GetString("Exists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO commercialapplication.action(productid, discount, thresholdamount, customerid)
        ///VALUES(@productid, @discount, @thresholdamount, @customerid).
        /// </summary>
        internal static string Insert {
            get {
                return ResourceManager.GetString("Insert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT discount, thresholdamount FROM commercialapplication.action.
        /// </summary>
        internal static string Select {
            get {
                return ResourceManager.GetString("Select", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM commercialapplication.action
        ///WHERE customerid = @customerId AND productid = @productid.
        /// </summary>
        internal static string SelectByCustomerId {
            get {
                return ResourceManager.GetString("SelectByCustomerId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT discount, thresholdamount FROM commercialapplication.action WHERE id = @id.
        /// </summary>
        internal static string SelectById {
            get {
                return ResourceManager.GetString("SelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT discount, thresholdamount FROM commercialapplication.action WHERE productid = @productid and customerid = @customerid.
        /// </summary>
        internal static string SelectByProductAndCustomerId {
            get {
                return ResourceManager.GetString("SelectByProductAndCustomerId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT discount, thresholdamount FROM commercialapplication.action WHERE productid = @productid.
        /// </summary>
        internal static string SelectByProductId {
            get {
                return ResourceManager.GetString("SelectByProductId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE commercialapplication.action
        ///SET productid = @productid, discount = @discount
        ///WHERE id = @id.
        /// </summary>
        internal static string Update {
            get {
                return ResourceManager.GetString("Update", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE commercialapplication.action
        ///SET discount = @discount, thresholdamount = @thresholdamount
        ///WHERE productid = @productid AND customerid = @customerid .
        /// </summary>
        internal static string UpdateByCustomerId {
            get {
                return ResourceManager.GetString("UpdateByCustomerId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE commercialapplication.action
        ///SET discount = @discount, thresholdamount = @thresholdamount
        ///WHERE productId = @productId AND customerid IN 
        ///(SELECT customerid
        ///FROM commercialapplication.customersaleschannel
        ///WHERE saleschannelid = @saleschannelid).
        /// </summary>
        internal static string UpdateBySalesChannelId {
            get {
                return ResourceManager.GetString("UpdateBySalesChannelId", resourceCulture);
            }
        }
    }
}
