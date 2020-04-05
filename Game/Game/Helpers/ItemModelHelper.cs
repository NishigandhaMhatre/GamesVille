using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Helpers
{
    /// <summary>
    /// Helper class that will be used since Item saved on the Character/Monster 
    /// is string and not ItemModel
    /// </summary>
    public static class ItemModelHelper
    {
        /// <summary>
        /// Gets the ItemModel from the guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ItemModel GetItemModelFromGuid(string id)
        {
            if(id != null)
            {
                return ItemIndexViewModel.Instance.Dataset.Where(a =>
                                        a.Id == id)
                                        .FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// Getst the Item's name from the guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetItemModelNameFromGuid(string id)
        {
            if (id != null)
            {
                ItemModel item = GetItemModelFromGuid(id);
                if (item == null)
                    return null;
                return item.Name;
            }
            return null;
        }
        /// <summary>
        /// Gets the list of item ids whose location is head
        /// </summary>
        public static List<string> GetHeadItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.Head)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
        /// <summary>
        /// Gets the list of item ids whose location is Necklace
        /// </summary>
        public static List<string> GetNecklaceItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.Necklass)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
        /// <summary>
        /// Gets the list of item ids whose location is PrimaryHand
        /// </summary>
        public static List<string> GetPrimaryHandItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.PrimaryHand)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
        /// <summary>
        /// Gets the list of item ids whose location is OffHand
        /// </summary>
        public static List<string> GetOffHandItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.OffHand)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
        /// <summary>
        /// Gets the list of item ids whose location is RightFinger
        /// </summary>
        public static List<string> GetRightFingerItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.RightFinger)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
        /// <summary>
        /// Gets the list of item ids whose location is LeftHand
        /// </summary>
        public static List<string> GetLeftFingerItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.LeftFinger)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
        /// <summary>
        /// Gets the list of item ids whose location is Feet
        /// </summary>
        public static List<string> GetFeetItemList
        {
            get
            {
                var myList = ItemIndexViewModel.Instance.Dataset.Where(item => item.Location.Equals(ItemLocationEnum.Feet)).Select(item => item.Id).ToList();
                myList.Add("0");
                return myList;
            }
        }
    }
}
