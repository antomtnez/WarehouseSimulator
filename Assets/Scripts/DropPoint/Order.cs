using System.Collections.Generic;

[System.Serializable]
    public class Order{
        [System.Serializable]
        public class OrderEntry{
            public string ItemId;
            public int CurrentStock;
            public int OrderedStock;
        }

        public List<OrderEntry> OrderInventory = new List<OrderEntry>();
        public int Reward = 0;
        public Order(ItemDatabase itemsDB){
            List<Item> itemsAvaliables = new List<Item>();
            foreach(Item item in itemsDB.ItemTypes)
                itemsAvaliables.Add(item);

            int itemsInOrder = UnityEngine.Random.Range(1, 4);

            for(int i=0; i < itemsInOrder; i++){
                int itemAvaliable = UnityEngine.Random.Range(0, itemsAvaliables.Count);
                OrderInventory.Add(new OrderEntry(){
                    ItemId = itemsAvaliables[itemAvaliable].Id,
                    CurrentStock = 0,
                    OrderedStock = UnityEngine.Random.Range(3,21)
                });
                itemsAvaliables.Remove(itemsAvaliables[itemAvaliable]);
                Reward += (itemsDB.GetItem(OrderInventory[i].ItemId).SellingPrice * OrderInventory[i].OrderedStock);
            }
        }

        public bool IsOrderCompleted(){
            foreach(OrderEntry orderEntry in OrderInventory){
                if(orderEntry.CurrentStock < orderEntry.OrderedStock) 
                    return false; 
            }

            return true;
        }
    }
