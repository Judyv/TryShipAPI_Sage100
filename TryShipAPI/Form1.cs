using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using TryShipAPI.StarShipShipAPI;

namespace TryShipAPI
{
    public partial class Form1 : Form
    {
        public SettingsManager settingsmanager;
        public ShipClient shippingclient;
        public StarShipRateAPI.DataTransactionsClient ratingclient;
        public Identity identity;
        public ClientAuthentication clientauthentication;
        public int CurrentLocationID = -1;

        public Form1()
        {
            InitializeComponent();
            lbLocationCode.Text = "";
            string datapath = Environment.ExpandEnvironmentVariables("%LocalAppData%") + "\\V-Technologies\\StarShip\\RateTool\\";
            if (System.IO.Directory.Exists(datapath) == false)
                System.IO.Directory.CreateDirectory(datapath);
            settingsmanager = new SettingsManager(datapath + "TryShipAPI.XML");
            edStarShipServer.Text = settingsmanager.Settings.StarShipServer;
            edSSUser.Text = settingsmanager.Settings.StarShipUser;
            edSSPassword.Text = settingsmanager.Settings.StarShipUser;
            edDevKey.Text = settingsmanager.Settings.DeveloperKey;
            linkLabel1.Text = "";

            if (settingsmanager.Settings.StarShipServer.Trim().Length > 0)
            {
                LoadLocations();
            }

        }

        private void LoadLocations()
        {
            try
            {
                cbSSLocation.DisplayMember = "Name";
                BasicHttpBinding basicbinding = new BasicHttpBinding();
                ratingclient = new StarShipRateAPI.DataTransactionsClient(basicbinding, new EndpointAddress("http://" + settingsmanager.Settings.StarShipServer + ":3315/WebServicesServer/Data"));
                StarShipRateAPI.LoadLocationsRequest loadlocationsrequest = new StarShipRateAPI.LoadLocationsRequest
                {
                    Identity = new StarShipRateAPI.Identity
                    {
                        ApplicationName = "Sample",
                        ApplicationVersion = "1.0",
                        DeveloperKey = ""
                    },
                    Authentication = null
                };
                StarShipRateAPI.LoadLocationsResponse locationsresponse = new StarShipRateAPI.LoadLocationsResponse();
                locationsresponse = ratingclient.LoadLocations(loadlocationsrequest);
                for (int i = 0; i < locationsresponse.Locations.Length; i++)
                {
                    cbSSLocation.Items.Add(locationsresponse.Locations[i]);
                }

                if (settingsmanager.Settings.StarShipLocation.Trim().Length > 0)
                {
                    cbSSLocation.SelectedIndex = cbSSLocation.FindStringExact(settingsmanager.Settings.StarShipLocation);
                }
                else
                    cbSSLocation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error loading locations : " + ex.Message);
            }

        }

        private void initconnection()
        {
            BasicHttpBinding basicbinding = new BasicHttpBinding();
            shippingclient = new ShipClient(basicbinding, new EndpointAddress("http://" + settingsmanager.Settings.StarShipServer + ":3316/Ship"));
            identity = new Identity()
            {
                ApplicationName = "TryShipAPI",
                ApplicationVersion = "1.0",
                DeveloperKey = settingsmanager.Settings.DeveloperKey
            };
            clientauthentication = new ClientAuthentication()
            {
                UserID = settingsmanager.Settings.StarShipUser,
                Password = settingsmanager.Settings.StarShipPassword,
                LocationID = settingsmanager.Settings.StarShipLocationID
            };
        }

        public void setlink(int ID)
        {
            linkLabel1.LinkVisited = false;
            linkLabel1.Text = "http://localhost:180/Shipments?ShipmentID=" + ID.ToString();
            //linkLabel1.LinkArea.Start = 1;
            //linkLabel1.LinkArea.Length = linkLabel1.Text.Length;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            // SHIPSHIPMENT
            initconnection();
            try
            {
                ShipShipmentRequest sRequest = new ShipShipmentRequest();
                sRequest.Identity = identity;
                sRequest.Authentication = clientauthentication;
                sRequest.Params = new ShipParams()
                {
                    AssignSSCC = AssignSSCCNumbers.All,
                    DeliverBy = new DateTime(),
                    WritebackToSource = false
                };
                sRequest.Shipment = new Shipment();
                sRequest.Shipment.Recipient = new Recipient()
                {
                    Address = new Address
                    {
                        Name = "Aaron Fitz Electrical",
                        Address1 = "2 N Michigan Ave",
                        Address2 = "Suite 405",
                        City = "Chicago",
                        StateProvinceCode = "IL",
                        PostalCode = "60602",
                        CountryCode = "US",
                        LocationType = LocationType.Business
                    },
                    Contact = new Contact
                    {
                        Name = "Misty Robinson",
                        Phone = "8605851400",
                        Email = "mistyr@gmail.com"
                    }
                };
                sRequest.Shipment.Sender = new Sender()
                {
                    Address = new Address
                    {
                        Name = "V-Technologies, LLC",
                        Address1 = "675 W Johnson Ave",
                        Address2 = "Suite 2000",
                        City = "Cheshire",
                        StateProvinceCode = "CT",
                        PostalCode = "06410",
                        CountryCode = "US",
                        LocationType = LocationType.Business
                    },
                    Contact = new Contact
                    {
                        Name = "James Mapes",
                        Phone = "8004624016",
                        Email = "jamesm@vtech.com"
                    },
                    AccountInfoList = new AccountInfo[1]
                };
                sRequest.Shipment.Sender.AccountInfoList[0] = new AccountInfo()
                {
                    AccountNumber = "V69W42", // "Default" doesn't work....
                    CarrierInterfaceID = "UPS",
                    SCAC = "UPS"
                };

                sRequest.Shipment.Billing = new Billing()
                {
                    BillingType = StarShipShipAPI.BillingType.Prepaid,
                    BillingOptionID = InternationalBillingOptionType.Transportation,
                    BillingAccountNumber = "V69W42",
                    BillDutiesTaxesType = BillDutiesTaxesType.Sender
                };

                sRequest.Shipment.ShipCarrier = new ShipCarrier()
                {
                    CarrierInterfaceID = "UPS",
                    CarrierName = "UPS",
                    SCAC = "UPS",
                    ServiceGroup = ServiceGroup.Ground,
                    CarrierType = CarrierType.Parcel,
                    ServiceID = "03",
                    ServiceName = "UPS® Ground",
                    AccountNumber = "V69W42"  // "Default" -- need account here
                };

                sRequest.Shipment.ShipDate = DateTime.Now;
                sRequest.Shipment.Packs = new Pack[1];
                sRequest.Shipment.Packs[0] = new Pack
                {
                    ActualWeight = new Weight
                    {
                        Value = 5.0M,
                        UOM = WeightUOMType.LB
                    },
                    Name = "Custom Box",
                    PackQty = 1,
                    PackagingType = PackagingTypeEnum.Box,
                    Dimensions = new Dimensions
                    {
                        Length = 10,
                        Height = 5,
                        Width = 5,
                        UOM = DimsUOMType.inch
                    }
                };
                ShipShipmentResponse sResponse = new ShipShipmentResponse();
                try
                {
                    sResponse = shippingclient.ShipShipment(sRequest);
                    if (sResponse.ResponseInfo.ResultCode.ToString() == "Failure")
                    {
                        System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + sResponse.ResponseInfo.Description.ToString());
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Success!");
                        setlink(sResponse.Shipment.ID);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + ex.Message);
                }
            }
            finally
            {
                shippingclient.Close();
            }

        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            initconnection();
            try
            {
                try
                {
                    // CREATESHIPMENT
                    CreateShipmentRequest cRequest = new CreateShipmentRequest();
                    cRequest.Authentication = clientauthentication;
                    cRequest.Identity = identity;
                    cRequest.SaveShipment = false;   // for speed, don't save shipment to StarShip DB (for test/debug, it helps to save it and see what you're getting)
                    cRequest.SourceDocument = new SourceDocument();
                    cRequest.SourceDocument.Company = "(ABC) ABC Distribution and Service Corp.";
                     cRequest.SourceDocument.DocumentKey = "0000184";
                    cRequest.SourceDocument.DocumentName = "Sales Orders";
                    cRequest.SourceDocument.SourceID = 90;
                    cRequest.SourceDocument.SourceName = "Sage 100 4.1 - 2020";
                    cRequest.SourceDocument.DocumentType = SourceDocumentType.Order;
                    cRequest.SourceDocument.Loaded = true;                      
                    // Need this to write back to source on ShipShipment
                    cRequest.SourceDocument.SourceAttributes = new SourceField[]
                    {
                        new SourceField{Name = "_WritebackSupported",Value = "True"}
                    };
                    cRequest.SourceDocument.HeaderFields = new SourceField[]
                    {
                        new SourceField{Name = "Sales Order Number", Value = "0000184"},
                        new SourceField{Name = "Customer PO Number", Value = "101255"},
                        new SourceField{Name = "Ship To Address Line 1", Value = "5411 Kendrick Dr"},
                        new SourceField{Name = "Ship To Address Line 2", Value = "Suite 2087"},
                        new SourceField{Name = "Ship-To Code", Value = "2"},
                        new SourceField{Name = "Confirm To", Value = "John Quinn"},
                        new SourceField{Name = "Ship To City", Value = "Racine"},
                        new SourceField{Name = "Ship To Name", Value = "American Business Futures"},
                        new SourceField{Name = "Ship To Country Name", Value = "United States of America"},
                        new SourceField{Name = "Customer Number", Value = "ABF"},
                        new SourceField{Name = "Residential Address", Value = "N"},
                        new SourceField{Name = "Ship To Zip Code", Value = "53402"},
                        new SourceField{Name = "Ship To State", Value = "WI"},
                        new SourceField{Name = "[Customer] Telephone Number", Value = "(414) 555-4787"},
                        new SourceField{Name = "Ship-From Address Line 1", Value = "10 McKee Place"},
                        new SourceField{Name = "Ship-From Address Line 2", Value = ""},
                        new SourceField{Name = "Ship-From City", Value = "Cheshire"},
                        new SourceField{Name = "Ship-From Company", Value = "ABC Districution and Service Corp."},
                        new SourceField{Name = "Ship-From ZIP Code", Value = "06410"},
                        new SourceField{Name = "Ship-From State", Value = "CT"},
                        new SourceField{Name = "Ship-From Phone", Value = "203-755-1212"},
                        new SourceField{Name = "Ship Via", Value = "UPS BLUE"}
                    };
                    // To access line items when adding packing, I mapped any unused field (here "Line Sequence Number") to "Extra Key 1"
                    // and assigned unique number 001, 002 (for real WMS application would be an internal key) this does two things:
                    //    1. prevents StarShip from rolling up lines with same item number and UOM  
                    //    2. allows me to access a specific line item within response when packing
                    cRequest.SourceDocument.LineItems = new SourceLineItem[]
                    {
                        new SourceLineItem
                            {
                                LineItemNumber = "1",
                                LineItemFields = new SourceField[]
                                {
                                    new SourceField{Name = "[Line Item] Line Sequence Number", Value = "001"},
                                    new SourceField{Name = "[Line Item] Item Code Description", Value = "HON 2 DRAWER LETTER FLE W/O LK"},
                                    new SourceField{Name = "[Line Item] Item Code", Value = "1001-HON-H252"},
                                    new SourceField{Name = "[Line Item] Quantity Ordered", Value = "2"},
                                    new SourceField{Name = "[Line Item] Quantity Shipped", Value = "0"},
                                    new SourceField{Name = "[Line Item] Unit of Measure", Value = "EACH"},
                                    new SourceField{Name = "[Line Item] Unit Price", Value = "84"},
                                    new SourceField{Name = "[Line Item] Unit Weight", Value = "35.0000"}
                                }
                            },
                            new SourceLineItem
                            {
                                LineItemNumber = "2",
                                LineItemFields = new SourceField[]
                                {
                                    new SourceField{Name = "[Line Item] Line Sequence Number", Value = "002"},
                                    new SourceField{Name = "[Line Item] Item Code Description", Value = "HON 4 DRAWER LETTER FLE W/O LK"},
                                    new SourceField{Name = "[Line Item] Item Code", Value = "1001-HON-H254"},
                                    new SourceField{Name = "[Line Item] Quantity Ordered", Value = "3"},
                                    new SourceField{Name = "[Line Item] Quantity Shipped", Value = "0"},
                                    new SourceField{Name = "[Line Item] Unit of Measure", Value = "EACH"},
                                    new SourceField{Name = "[Line Item] Unit Price", Value = "131"},
                                    new SourceField{Name = "[Line Item] Unit Weight", Value = "0.0000"}
                                }
                            }
                    };
                    
                    CreateShipmentResponse Response = new CreateShipmentResponse();
                    try
                    {
                        Response.Shipment = new Shipment();
                        Response = shippingclient.CreateShipment(cRequest);
                        if (Response.ResponseInfo.ResultCode.ToString() == "Failure")
                        {
                            throw new Exception("CreateShipment returned failure " + Response.ResponseInfo.Description.ToString());
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("CreateShipment returned Success!");
                            setlink(Response.Shipment.ID);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error calling CreateShipment : " + ex.Message);
                    }

                    // now we can add packaging in returned shipment and call ShipShipment
                    Response.Shipment.Packs = new Pack[]
                    {
                        // We are going to create thre packages containing the three line items as follows
                        // Note: the value I sent in for field "Extra 1" gets returned as "ExtraKey" due to my customized interface mapping
                        //
                        // Pkg   Item Number   ExtraKey    Qty
                        // 1     1001-HON-H252    001      1
                        // 1     1001-HON-H254    002      1
                        //
                        // 2     1001-HON-H252    001      1
                        // 2     1001-HON-H254    002      2                    
                        //                    
                        new Pack
                        {

                            ActualWeight = new Weight
                            {
                                UOM = WeightUOMType.LB,
                                Value = 10.5M
                            },
                            Dimensions = new Dimensions
                            {
                                Length = 10,
                                Height = 5,
                                Width = 10
                            },
                            Name = "Big Box",
                            PackagingType = PackagingTypeEnum.Box,
                            PackQty = 1,
                            DocumentKey = "0000184",
                            PalletID = -1,
                            LineItems = new LineItem[]
                            {
                                new LineItem
                                {
                                    ItemID = Response.Shipment.LineItems.FirstOrDefault(s => s.ExtraKey.Equals("001")).ItemID,
                                    OrderNumber = "0000184",
                                    ShipQty = 1
                                },
                                new LineItem
                                {
                                    ItemID = Response.Shipment.LineItems.FirstOrDefault(s => s.ExtraKey.Equals("002")).ItemID,
                                    OrderNumber = "0000184",
                                    ShipQty = 1
                                }
                            }
                        },
                        new Pack
                        {

                            ActualWeight = new Weight
                            {
                                UOM = WeightUOMType.LB,
                                Value = 15.5M
                            },
                            Dimensions = new Dimensions
                            {
                                Length = 12,
                                Height = 6,
                                Width = 11
                            },
                            Name = "Medium Carton",
                            PackagingType = PackagingTypeEnum.Box,
                            PackQty = 1,
                            DocumentKey = "0000184",
                            PalletID = -1,
                            LineItems = new LineItem[]
                            {
                                new LineItem
                                {
                                    ItemID = Response.Shipment.LineItems.FirstOrDefault(s => s.ExtraKey.Equals("001")).ItemID,
                                    OrderNumber = "0000184",
                                    ShipQty = 1
                                },
                                new LineItem
                                {
                                    ItemID = Response.Shipment.LineItems.FirstOrDefault(s => s.ExtraKey.Equals("002")).ItemID,
                                    OrderNumber = "0000184",
                                    ShipQty = 2
                                }
                            }
                        }
                    };
                    Response.Shipment.FSIDocInfo.ShipperID = "JV";


                    // SHIPSHIPMENT
                    ShipShipmentRequest sRequest = new ShipShipmentRequest();
                    sRequest.Identity = identity;
                    sRequest.Authentication = clientauthentication;
                    sRequest.Params = new ShipParams()
                    {
                        AssignSSCC = AssignSSCCNumbers.All,
                        DeliverBy = new DateTime(),
                        WritebackToSource = false                 // JV WRITE BACK
                    };
                    sRequest.Shipment = Response.Shipment;
                    sRequest.Shipment.FSIDocInfo.WritebackFreightCharges = true;

                    ShipShipmentResponse sResponse = new ShipShipmentResponse();
                    try
                    {
                        sResponse = shippingclient.ShipShipment(sRequest);
                        if (sResponse.ResponseInfo.ResultCode.ToString() == "Failure")
                        {
                            System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + sResponse.ResponseInfo.Description.ToString());
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("ShipShipment Successful!");
                            setlink(sResponse.Shipment.ID);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + ex.Message);
                    }
                   
                }
                catch (Exception ex)
                {
                     System.Windows.Forms.MessageBox.Show("Error : " + ex.Message);
                }
            }
            finally
            {
                shippingclient.Close();
            }
            
        }

        private void btShipSourceDocument_Click(object sender, EventArgs e)
        {
            initconnection();
            try
            {
                try
                {
                    // SHIPSOURCEDOCUMENT 
                    ShipSourceDocumentRequest cRequest = new ShipSourceDocumentRequest();
                    cRequest.Identity = identity;
                    cRequest.Authentication = clientauthentication;
                    cRequest.Params = new ShipParams()
                    {
                        AssignSSCC = AssignSSCCNumbers.All,
                        DeliverBy = new DateTime(),
                        WritebackToSource = false     // JV WRITE BACK
                    };
                    cRequest.Document = new SourceDocument();
                    cRequest.Document.Company = "(ABC) ABC Distribution and Service Corp.";
                    cRequest.Document.DocumentKey = "0100058";
                    cRequest.Document.DocumentName = "Invoice";
                    cRequest.Document.SourceID = 90;
                    cRequest.Document.SourceName = "Sage 100 4.1 - 2020";
                    cRequest.Document.DocumentType = SourceDocumentType.Shipment;
                    cRequest.Document.Loaded = true;
                    cRequest.Document.SourceAttributes = new SourceField[]
                    {
                        new SourceField{Name = "_WritebackSupported",Value = "True"}
                    };
                    cRequest.Document.HeaderFields = new SourceField[]
                    {
                        new SourceField{Name = "Sales Order Number", Value = "0000181"},
                        new SourceField{Name = "Customer PO Number", Value = "243254"},
                        new SourceField{Name = "Ship-To Address 1", Value = "Racine Warehouse"},
                        new SourceField{Name = "Ship-To Address 2", Value = "5411 Kendrick Dr."},
                        new SourceField{Name = "Ship-To Code", Value = "2"},
                        new SourceField{Name = "Confirm To", Value = "John Quinn"},
                        new SourceField{Name = "Ship-To City", Value = "Racine"},
                        new SourceField{Name = "Ship-To Name", Value = "American Business Futures"},
                        new SourceField{Name = "Ship To Country Name", Value = "United States"},
                        new SourceField{Name = "Customer Number", Value = "ABF"},
                        new SourceField{Name = "Email Address", Value = "artie@sage.sample.com"},
                        new SourceField{Name = "Ship-To Zip Code", Value = "53402"},
                        new SourceField{Name = "Ship-To State", Value = "WI"},
                        new SourceField{Name = "Ship To Telephone Number", Value = "(414) 555-4319"},
                        new SourceField{Name = "Ship-From Address 1", Value = "10 McKee Place"},
                        new SourceField{Name = "Ship-From Address Line 2", Value = ""},
                        new SourceField{Name = "Ship-From City", Value = "Cheshire"},
                        new SourceField{Name = "Ship-From Company", Value = "ABC Districution and Service Corp."},
                        new SourceField{Name = "Ship-From ZIP Code", Value = "06410"},
                        new SourceField{Name = "Ship-From State", Value = "CT"},
                        new SourceField{Name = "Ship-From Phone", Value = "203-755-1212"},
                        new SourceField{Name = "Ship Via", Value = "UPS BLUE"}                        
                    };                    
                    cRequest.Document.InnerOrders = new Document[]
                    {
                        new Document
                        {
                            DocumentKey = "0000181",
                            DocumentType = SourceDocumentType.Order,
                            HeaderFields = new SourceField[]
                            {
                                new SourceField{Name = "Order No.", Value = "0000181"},
                                new SourceField{Name = "PO Number", Value = "243254"},
                                new SourceField{Name = "DocumentKey", Value = "0000181"},
                            },
                            // by default, Staship matches up items in package to lines in order by order number, item number, and UOM. 
                            // You can create an item level udf and map to  StarShip's "Extra Key 1" and assigned a unique numeric value to it.
                            // This will make my packing precise even if multiple lines in the same order share the same item number and UOM.
                            LineItems = new SourceLineItem[]
                            {
                            new SourceLineItem
                            {
                                LineItemNumber = "1",
                                LineItemFields = new SourceField[]
                                {
                                    new SourceField{Name = "[Line Item] Item Code Description", Value = "PRINTER STAND W/ BASKET"},
                                    new SourceField{Name = "[Line Item] Item Code", Value = "6655"},
                                    new SourceField{Name = "[Line Item] Quantity Ordered", Value = "2"},
                                    new SourceField{Name = "[Line Item] Unit Of Measure", Value = "EACH"},
                                    new SourceField{Name = "[Line Item] Unit Price", Value = "179"},
                                    new SourceField{Name = "[Line Item] Unit Weight", Value = "0.000"},
                                    new SourceField{Name = "[Line Item] Quantity Shipped", Value = "2"}                                   
                                }
                            },
                            new SourceLineItem
                            {
                                LineItemNumber = "2",
                                LineItemFields = new SourceField[]
                                {
                                    new SourceField{Name = "[Line Item] Item Code Description", Value = "2.5 320GB External Portable "},
                                    new SourceField{Name = "[Line Item] Item Code", Value = "8983"},
                                    new SourceField{Name = "[Line Item] Quantity Ordered", Value = "2"},
                                    new SourceField{Name = "[Line Item] Unit Of Measure", Value = "EACH"},
                                    new SourceField{Name = "[Line Item] Unit Price", Value = "41.59"},
                                    new SourceField{Name = "[Line Item] Unit Weight", Value = "0.000"},
                                    new SourceField{Name = "[Line Item] Quantity Shipped", Value = "2"}
                                }
                            }
                            
                            }
                        }
                        // you can add additional documents here!
                    };

                    // We are going to create two packages containing the two line items as follows
                    //
                    // Pkg   Item Number   Qty
                    // 1     6655           2
                    // 2     8983           2                    
                    
                    cRequest.Document.Packages = new SourcePackage[]
                    {
                        new SourcePackage
                        {
                            PackageID = "1",
                            PackageFields = new SourceField[]
                            {
                                new SourceField{Name = "[UDF][package] Height", Value = "10"},
                                new SourceField{Name = "[UDF][package] Length", Value = "10"},
                                new SourceField{Name = "[UDF][package] Width", Value = "10"},
                                new SourceField{Name = "[UDF][package] Weight", Value = "3"},                                
                            },
                            Content = new PackageContent[]
                            {
                                new PackageContent
                                {
                                    ContentFields = new SourceField[]
                                    {
                                        new SourceField{Name = "Package Content Item Number", Value = "6655"},
                                        new SourceField{Name = "Package Content Quantity", Value = "2"},
                                        new SourceField{Name = "Package Content Sales Order", Value = "0000181"},                                        
                                    }
                                }
                            }
                        },
                        new SourcePackage
                        {
                            PackageID = "2",
                            PackageFields = new SourceField[]
                            {
                                new SourceField{Name = "[UDF][package] Height", Value = "12"},
                                new SourceField{Name = "[UDF][package] Length", Value = "12"},
                                new SourceField{Name = "[UDF][package] Width", Value = "12"},
                                new SourceField{Name = "[UDF][package] Weight", Value = "4"},                                
                            },
                            Content = new PackageContent[]
                            {
                                new PackageContent
                                {
                                    ContentFields = new SourceField[]
                                    {
                                        new SourceField{Name = "Package Content Item Number", Value = "8983"},
                                        new SourceField{Name = "Package Content Quantity", Value = "2"},
                                        new SourceField{Name = "Package Content Sales Order", Value = "0000181"},                                        
                                    }
                                }
                            }
                        
                        }
                    };

                    ShipShipmentResponse sResponse = new ShipShipmentResponse();
                    try
                    {
                        sResponse = shippingclient.ShipSourceDocument(cRequest);
                        if (sResponse.ResponseInfo.ResultCode.ToString() == "Failure")
                        {
                            System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + sResponse.ResponseInfo.Description.ToString());
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("ShipShipment Successful!");
                            setlink(sResponse.Shipment.ID);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error : " + ex.Message);
                }
            }
            finally
            {
                shippingclient.Close();
            }
        }

        private void cbSSLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsmanager.Settings.StarShipLocation = cbSSLocation.Text;
            settingsmanager.Settings.StarShipLocationID = (cbSSLocation.SelectedItem as StarShipRateAPI.Location).Code;
            lbLocationCode.Text = settingsmanager.Settings.StarShipLocationID.ToString();
        }


        private void edStarShipServer_Leave(object sender, EventArgs e)
        {
            if (settingsmanager.Settings.StarShipServer.Trim().ToUpper() != edStarShipServer.Text.Trim().ToUpper())
            {
                settingsmanager.Settings.StarShipServer = edStarShipServer.Text.Trim();
                LoadLocations();
            };
            checkvalid();
        }

        private void edSSUser_TextChanged(object sender, EventArgs e)
        {
            settingsmanager.Settings.StarShipUser = edSSUser.Text;
            checkvalid();
        }

        private void edSSPassword_TextChanged(object sender, EventArgs e)
        {
            settingsmanager.Settings.StarShipPassword = edSSPassword.Text;
            checkvalid();
        }

        private void edDevKey_TextChanged(object sender, EventArgs e)
        {
            settingsmanager.Settings.DeveloperKey = edDevKey.Text;
            checkvalid();
        }

        private void checkvalid()
        {
            if (settingsmanager.SettingsAreValid())
            {
                btCreate.Enabled = true;
                btShipShipment.Enabled = true;
                btShipSourceDocument.Enabled = true;
            }
            else
            {
                btCreate.Enabled = false;
                btShipShipment.Enabled = false;
                btShipSourceDocument.Enabled = false;
            };
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            settingsmanager.StoreSettings();
        }

        private void edStarShipServer_TextChanged(object sender, EventArgs e)
        {
            if (edStarShipServer.Text != settingsmanager.Settings.StarShipServer)
            {
                cbSSLocation.Items.Clear();
                cbSSLocation.Text = "";
                lbLocationCode.Text = "";
            }
        }

        private void ShipSourceByID_Click(object sender, EventArgs e)
        {
            initconnection();
            try
            {
                // ShipSourceDocumentByID
                ShipShipmentResponse sResponse = new ShipShipmentResponse();
                try
                {
                    ShipSourceDocumentByIDRequest sRequest = new ShipSourceDocumentByIDRequest();
                    sRequest.Authentication = clientauthentication;
                    sRequest.Identity = identity;
                    sRequest.Params = new ShipParams()
                    {
                        AssignSSCC = AssignSSCCNumbers.All,
                        DeliverBy = new DateTime(),
                        WritebackToSource = true
                    };
                    sRequest.CompanyName = "(ABC) ABC Distribution and Service Corp.";
                    sRequest.DocumentKey = "0100058";
                    sRequest.DocumentName = "Invoice";
                    sRequest.SourceID = 90;
                    sResponse = shippingclient.ShipSourceDocumentByID(sRequest);
                    if (sResponse.ResponseInfo.ResultCode.ToString() == "Failure")
                    {
                        System.Windows.Forms.MessageBox.Show("Error in ShipShipment : " + sResponse.ResponseInfo.Description.ToString());
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("ShipShipment Successful!");
                        setlink(sResponse.Shipment.ID);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error in ShipSourceDocumentByID : " + ex.Message);
                }
            }
            finally
            {
                shippingclient.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {                
                linkLabel1.LinkVisited = true;
                //Call the Process.Start method to open the default browser
                //with a URL:
                System.Diagnostics.Process.Start(linkLabel1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked." + ex.Message);
            }
        }
    }
}
