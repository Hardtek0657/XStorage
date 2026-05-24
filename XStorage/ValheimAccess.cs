using HarmonyLib;
using System.Reflection;

namespace XStorage
{
    internal static class ValheimAccess
    {
        private static readonly MethodInfo ContainerCheckAccessMethod = AccessTools.Method(typeof(Container), "CheckAccess");
        private static readonly FieldInfo InventoryNameField = AccessTools.Field(typeof(Inventory), "m_name");
        private static readonly FieldInfo InventoryGuiCurrentContainerField = AccessTools.Field(typeof(InventoryGui), "m_currentContainer");
        private static readonly MethodInfo InventoryGuiOnRightClickItemMethod = AccessTools.Method(typeof(InventoryGui), "OnRightClickItem");
        private static readonly MethodInfo InventoryGuiOnSelectedItemMethod = AccessTools.Method(typeof(InventoryGui), "OnSelectedItem");
        private static readonly MethodInfo InventoryGuiUpdateCraftingPanelMethod = AccessTools.Method(typeof(InventoryGui), "UpdateCraftingPanel");
        private static readonly MethodInfo InventoryGuiSetupDragItemMethod = AccessTools.Method(typeof(InventoryGui), "SetupDragItem");
        private static readonly MethodInfo ZRoutedRpcGetServerPeerIdMethod = AccessTools.Method(typeof(ZRoutedRpc), "GetServerPeerID");

        internal static bool CheckContainerAccess(this Container container, long playerId)
        {
            return (bool)ContainerCheckAccessMethod.Invoke(container, new object[] { playerId });
        }

        internal static void SetInventoryName(this Inventory inventory, string name)
        {
            InventoryNameField.SetValue(inventory, name);
        }

        internal static Container GetCurrentContainer(this InventoryGui inventoryGui)
        {
            return (Container)InventoryGuiCurrentContainerField.GetValue(inventoryGui);
        }

        internal static void InvokeOnRightClickItem(this InventoryGui inventoryGui, InventoryGrid grid, ItemDrop.ItemData item, Vector2i pos)
        {
            InventoryGuiOnRightClickItemMethod.Invoke(inventoryGui, new object[] { grid, item, pos });
        }

        internal static void InvokeOnSelectedItem(this InventoryGui inventoryGui, InventoryGrid grid, ItemDrop.ItemData item, Vector2i pos, InventoryGrid.Modifier mod)
        {
            InventoryGuiOnSelectedItemMethod.Invoke(inventoryGui, new object[] { grid, item, pos, mod });
        }

        internal static void InvokeUpdateCraftingPanel(this InventoryGui inventoryGui)
        {
            InventoryGuiUpdateCraftingPanelMethod.Invoke(inventoryGui, null);
        }

        internal static void InvokeSetupDragItem(this InventoryGui inventoryGui, ItemDrop.ItemData item, Inventory inventory, int amount)
        {
            InventoryGuiSetupDragItemMethod.Invoke(inventoryGui, new object[] { item, inventory, amount });
        }

        internal static long GetServerPeerId(this ZRoutedRpc routedRpc)
        {
            return (long)ZRoutedRpcGetServerPeerIdMethod.Invoke(routedRpc, null);
        }
    }
}
