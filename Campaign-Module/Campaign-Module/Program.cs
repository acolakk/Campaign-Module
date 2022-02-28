using Campaign_Module.Application;
using Campaign_Module.Domain.AggregateModels;
using Campaign_Module.Infrastructure.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Campaign_Module
{
    class Program
    {
        public static int time = 0;
        public static int stock = -1;
        static void Main(string[] args)
        {
            CampaignCalculator campaignCalculator = new CampaignCalculator();
            RepositoryProvider provider = new RepositoryProvider();
            string status = "";
            while (true)
            {

                Console.WriteLine("Enter Command :");
                string[] parameters = Console.ReadLine().Split(' ');
                string command = parameters[0];
                if (command == "create_product")
                {
                    Product product = new Product()
                    {
                        ProductCode = parameters[1],
                        Price = Convert.ToInt32(parameters[2]),
                        Stock = Convert.ToInt32(parameters[3])
                    };
                    var result = provider.ProductRepository.Add(product);
                    if (result.Result)
                    {
                        Console.WriteLine($"Product created; code {product.ProductCode}, price {product.Price}, stock {product.Stock}");
                    }
                    else
                    {
                        Console.WriteLine(result.Message);

                    }
                }
                else if (command == "get_product_info")
                {
                    string productCode = parameters[1];
                    var productData = provider.ProductRepository.GetByProductCode(productCode);
                    int price = campaignCalculator.Calculate(productData);
                    int processedStock = (stock == -1) ? productData.Stock : stock;
                    Console.WriteLine($"Product {productData.ProductCode} info; price {price}, stock {processedStock}");
                }
                else if (command == "create_order")
                {
                    Order order = new Order()
                    {
                        ProductCode = parameters[1],
                        Quantity = Convert.ToInt32(parameters[2]),

                    };
                    var productData = provider.ProductRepository.GetByProductCode(order.ProductCode);
                    var campaingData = provider.CampaignRepository.Get(x => x.ProductCode == order.ProductCode).FirstOrDefault();

                    if (productData != null)
                    {
                        order.Price = campaignCalculator.Calculate(productData);
                        if (campaingData != null)
                        {
                            order.CampaignName = campaingData.CampaignName;
                        }

                        if (productData.Stock >= order.Quantity)
                        {
                            provider.OrderRepository.Add(order);
                            stock = productData.Stock - order.Quantity;
                            Console.WriteLine($"Order created; product {order.ProductCode}, quantity {order.Quantity}");
                        }
                        else
                        {
                            Console.WriteLine("No stock");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Order could not be created because there is no product");
                    }

                }
                else if (command == "create_campaign")
                {

                    string productCode = parameters[2];

                    var prdoctData = provider.ProductRepository.GetByProductCode(productCode);
                    var campainData = provider.CampaignRepository.Get(x => x.ProductCode == productCode).FirstOrDefault();

                    if (prdoctData != null)
                    {
                        if (campainData == null)
                        {
                            Campaign campaign = new Campaign()
                            {
                                CampaignName = parameters[1],
                                ProductCode = productCode,
                                Duration = Convert.ToInt32(parameters[3]),
                                PriceManipulationLimit = Convert.ToInt32(parameters[4]),
                                TargetSalesCount = Convert.ToInt32(parameters[5])
                            };
                            provider.CampaignRepository.Add(campaign);

                            Console.WriteLine($"Campaign created; name {campaign.CampaignName}, product {campaign.ProductCode}, duration {campaign.Duration},limit {campaign.PriceManipulationLimit}, target sales count {campaign.TargetSalesCount}");
                        }
                        else
                        {
                            Console.WriteLine("There is a campaign for this product");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Campaign could not be created because there is no product");
                    }

                }
                else if (command == "get_campaign_info")
                {
                    string campainName = parameters[1];

                    var campaignData = provider.CampaignRepository.Get(x => x.CampaignName == campainName).FirstOrDefault();
                    var orders = provider.OrderRepository.Get(x => x.CampaignName == campainName).ToList();
                    var averageItemPrice = orders.Select(x => x.Price).Average();
                    var totalSales = orders.Count();

                    if (campaignData.Duration >= time)
                        status = "Active";
                    else
                        status = "Ended";

                    Console.WriteLine($"Campaign {campaignData.CampaignName} info; Status {status}, Target Sales {campaignData.TargetSalesCount},Total Sales {totalSales}, Average Item Price {averageItemPrice}");

                }
                else if (command == "increase_time")
                {
                    time = Convert.ToInt32(parameters[1]) + time;
                    Console.WriteLine($"Time is 0{time}:00");
                }
            }
        }
    }
}
