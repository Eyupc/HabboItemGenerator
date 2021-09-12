using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient; 
using System.Threading.Tasks;

namespace HabboItemGenerator.utils
{
    class SQLGenerator
    {
        private MySqlCommand command;
        private MySqlDataReader reader;
        private MySqlConnection connection;

        private Dictionary<string, string> itemDetails = new();

        public static int itemID = 0;

        private static StringBuilder catalog_items = new();
        private static StringBuilder items_base = new();

        public SQLGenerator(Database connection, Dictionary<string, string> itemDetails)
        {
            this.command = connection.getCommand();
            this.reader = connection.getDataReader();

            this.connection = connection.getConnection();
            this.itemDetails = itemDetails;

            if (itemID == 0)
            itemID += getLastItemID() + 1;
            else
            itemID++;

            this.inserItemsBase();
            this.insertCatalogItems();

        }


        private void inserItemsBase()
        {
            String sql = "INSERT INTO `items_base` (`id`, `sprite_id`, `item_name`, `public_name`, `width`, `length`, `stack_height`, `allow_stack`, `allow_sit`, `allow_lay`, `allow_walk`, `allow_gift`, `allow_trade`, `allow_recycle`, `allow_marketplace_sell`, `allow_inventory_stack`, `type`, `interaction_type`, `interaction_modes_count`, `vending_ids`, `multiheight`, `customparams`, `effect_id_male`, `effect_id_female`, `clothing_on_walk`) " +
                "VALUES(" + itemID + ", " + itemID + ", '" + itemDetails["name"] + "', '" + itemDetails["name"] + "',"+itemDetails["X"]+","+ itemDetails["Y"]+","+ itemDetails["Z"]+", '1', '0', '0', '0', '1', '1', '1', '1', '1', 's', 'default',"+ itemDetails["interaction_modes_count"] + ", '', '', '', 0, 0, ''); ";

            items_base.AppendLine(sql);
        }

        private void insertCatalogItems()
        {
            String sql = "INSERT INTO `catalog_items` (`id`, `item_ids`, `page_id`, `catalog_name`, `cost_credits`, `cost_points`, `points_type`, `amount`, `limited_stack`, `limited_sells`, `order_number`, `offer_id`, `song_id`, `extradata`, `have_offer`, `club_only`) VALUES (" + itemID + ", '" + itemID + "', " + "{PAGEID}" + ", '" + itemDetails["name"] + "', 3, 0, 0, 1, 0, 0, 1, " + itemID + ", 0, '', '1', '0');";

            catalog_items.AppendLine(sql);
        }

        public String getItemsBase()
        {
            return items_base.ToString();
        }

        public String getCatalogItems()
        {
            return catalog_items.ToString();
        }

        private int getLastItemID()
        {

            using (MySqlCommand _command = this.command)
            {
                _command.CommandText = "SELECT id FROM items_base WHERE id=(SELECT max(id) FROM items_base);";
                using (MySqlDataReader _reader = _command.ExecuteReader())
                {
                    _reader.Read();
                    return (int)_reader.GetInt64(0);
                }
            }

        }

    }
}
