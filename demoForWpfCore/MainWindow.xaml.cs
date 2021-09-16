using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demoForWpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitConfig();
            Loaded += MainWindow_Loaded;
        }

        private static string accessKeyId;
        private static string accessKeySecret;
        private static string endpoint;

        private void InitConfig()
        {
            accessKeyId = ConfigurationManager.AppSettings["AccessKeyId"] ?? string.Empty;
            accessKeySecret = ConfigurationManager.AppSettings["AccessKeySecret"] ?? string.Empty;
            endpoint = ConfigurationManager.AppSettings["Endpoint"] ?? string.Empty;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 由用户指定的OSS访问地址、阿里云颁发的AccessKeyId/AccessKeySecret构造一个新的OssClient实例。
            var ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                var buckets = ossClient.ListBuckets();
                ListViewForBucket.ItemsSource = buckets;

                Console.WriteLine("List bucket succeeded");
                foreach (var bucket in buckets)
                {
                    Console.WriteLine("Bucket name：{0}，Location：{1}，Owner：{2}", bucket.Name, bucket.Location, bucket.Owner);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("List bucket failed. {0}", ex.Message);
            }
        }
    }
}
