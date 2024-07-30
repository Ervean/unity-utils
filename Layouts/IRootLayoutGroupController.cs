using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ervean.Utilities.UI
{
    /// <summary>
    /// Used for communication between nested layout groups, where you would want to resolve any conflicting layouts 
    /// refresh all children layouts, then parents. This should be placed on a script that can update the root layout group of an object. 
    /// All other children scripts should have a reference to the group controller.
    /// </summary>
    public interface IRootLayoutGroupController 
    {
        /// <summary>
        /// This should perform a reverse breath- first update on layout group items
        /// </summary>
        void RequestUpdateLayout();

        event EventHandler<UpdateLayoutEventArgs> UpdateLayout;
    }

    public class UpdateLayoutEventArgs
    {

    }

    /// <summary>
    /// Should be placed on any script that has a layout group, used by the root layout group controller to update
    /// </summary>
    public interface ILayoutGroupItem
    {
        IRootLayoutGroupController RootController { get; }
        /// <summary>
        /// Set up events here
        /// </summary>
        /// <param name="controller"></param>
        void SetRootLayoutGroupController(IRootLayoutGroupController controller);
        void UpdateLayout();
    }
}
