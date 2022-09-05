using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TeamweDev_GP_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        User Login(string email, string password);

        [OperationContract]
        int register(User user);

        [OperationContract]
        List<Product> getAllProducts();

        [OperationContract]
        Product getProduct(int ID);

        [OperationContract]
        List<Product> getAllProductsAlphabetically();

        [OperationContract]
        List<Product> getAllProductsByPrice();

        [OperationContract]
        List<Product> getAllProductsPriceFiltered(int min, int max);

        [OperationContract]
        List<Product> getAllProductsCategoryFiltered(char aORnORe);
    }
}
