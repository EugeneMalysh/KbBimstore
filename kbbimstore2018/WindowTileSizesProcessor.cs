
using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Windows.Automation;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace KbBimstore
{
    public enum TileType { Center, Left, Right, Top, Bottom };

    public enum TileAction { Left, Right, Top, Bottom, Bigger, Smaller, Load, Save };

    public class WindowTileSizesProcessor
    {
        private Autodesk.Revit.DB.Document doc;
        private Autodesk.Revit.UI.UIDocument uidoc;
        private Autodesk.Revit.UI.UIApplication uiapp;
        private Rectangle drawAreaRectangle;
        private Rectangle mainWindowRectangle;

        public TileType tyleType = TileType.Center;
        public int tyleStep = 70;

        public WindowTileSizesProcessor()
        {

        }

        private bool almEnabled = false;
        public void setAlmEnabled(bool enable)
        {
            this.almEnabled = enable;
        }

        private List<string> getOpenPlanViewsTitles()
        {
            List<string> planViewsTitles = new List<string>();

            if (this.uidoc != null)
            {
                Document doc = this.uidoc.Document;
                if (doc != null)
                {
                    IList<UIView> uiviews = this.uidoc.GetOpenUIViews();
                    IList<UIView> cluiviews = new List<UIView>();

                    for (int i = 0; i < uiviews.Count; i++)
                    {
                        UIView curUIView = uiviews[i];
                        ElementId curViewId = curUIView.ViewId;
                        if (curViewId != null)
                        {

                            Element curElement = doc.GetElement(curViewId);
                            if (curElement != null)
                            {
                                string curElementTypeName = curElement.GetType().Name;
                                if (curElementTypeName == "ViewPlan")
                                {
                                    ViewPlan curViewPlan = curElement as ViewPlan;
                                    if (curViewPlan != null)
                                    {
                                        Autodesk.Revit.DB.ElementId curElementId = curViewPlan.GetTypeId();
                                        Autodesk.Revit.DB.ElementType curElementType = doc.GetElement(curElementId) as ElementType;

                                        if (curElementType != null)
                                        {
                                            if (curElementType.GetType().Name == "ViewFamilyType")
                                            {
                                                Autodesk.Revit.DB.ViewFamilyType curViewFamilyType = (ViewFamilyType)curElementType;

                                                if (curViewFamilyType != null)
                                                {
                                                    string curTitle = curViewFamilyType.Name + ": " + curViewPlan.Name;
                                                    planViewsTitles.Add(curTitle);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    cluiviews.Add(curUIView);
                                }
                            }
                        }
                    }

                    if (cluiviews.Count > 1)
                    {
                        while (cluiviews.Count > 0)
                        {
                            try
                            {
                                UIView curUIView = cluiviews.ElementAt(0);
                                cluiviews.RemoveAt(0);
                                if (curUIView != null)
                                {
                                    try
                                    {
                                        if (cluiviews.Count > 1)
                                        {
                                            curUIView.Close();
                                            curUIView.Dispose();
                                        }
                                    }
                                    catch (Exception exA)
                                    {
                                    }
                                }
                            }
                            catch (Exception exB)
                            {
                            }
                        }
                    }
                }
            }

            return planViewsTitles;

        }

        public void init(Autodesk.Revit.UI.UIApplication uiapp, TileAction tileAction)
        {
            this.uiapp = uiapp;
            this.uidoc = this.uiapp.ActiveUIDocument;
            this.doc = this.uidoc.Document;
            this.drawAreaRectangle = this.uiapp.DrawingAreaExtents;
            this.mainWindowRectangle = this.uiapp.MainWindowExtents;

            List<string> planViewsTitles = getOpenPlanViewsTitles();

            if (this.almEnabled)
            {
                try
                {
                    switch (tileAction)
                    {
                        case TileAction.Left:
                            setWindowsTileLeft();
                            break;
                        case TileAction.Right:
                            setWindowsTileRight();
                            break;
                        case TileAction.Top:
                            setWindowsTileTop();
                            break;
                        case TileAction.Bottom:
                            setWindowsTileBottom();
                            break;
                        case TileAction.Bigger:
                            setWindowsTileBigger();
                            break;
                        case TileAction.Smaller:
                            setWindowsTileSmaller();
                            break;
                        case TileAction.Load:
                            setWindowsTileLoad();
                            break;
                        case TileAction.Save:
                            setWindowsTileSave();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        /// <summary>
        /// FindByMultipleConditions
        /// поиск дочерних окон, зная главное окно
        /// </summary>
        /// <param name="elementWindowElement"></param>
        /// <returns></returns>
        AutomationElementCollection FindByMultipleConditions(AutomationElement elementWindowElement)
        {
            if (elementWindowElement == null)
            {
                throw new ArgumentException();
            }

            Condition conditions = new OrCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
      //);

            // Find all children that match the specified conditions.
            AutomationElementCollection elementCollection =
                elementWindowElement.FindAll(TreeScope.Children, conditions);
            return elementCollection;
        }
        /// <summary>
        /// setWindowsTileLeft
        /// debug this method only!!!
        /// </summary>
        private void setWindowsTileLeft()
        {
            //
            //UIApplication uiapp = commandData.Application;
            //UIDocument uidoc = uiapp.ActiveUIDocument;
            //Document doc = uidoc.Document;
            //View view = doc.ActiveView;
            //UIView uiview = null;
            //IList<UIView> uiviews = uidoc.GetOpenUIViews();

            //foreach (UIView uv in uiviews)
            //{
            //    if (uv.ViewId.Equals(view.Id))
            //    {
            //        uiview = uv;
            //        break;
            //    }
            //}
            //
          
            
            Process[] processes = Process.GetProcessesByName("Revit");

            if (0 < processes.Length)
            {
                //главное окно процесса
                IntPtr mainWinHandle = processes[0].MainWindowHandle;
                if (mainWinHandle != null)
                {
                    //его приведение к типу
                    AutomationElement MainWndAutElem = AutomationElement.FromHandle(mainWinHandle);
                    if (MainWndAutElem != null)
                    {
                        Condition MainWndConditions = new AndCondition(
                          new PropertyCondition(AutomationElement.ClassNameProperty, "MDIClient"),
                          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
                          );
                        //
                        //AutomationElementCollection subwindows =
                        //MainWndAutElem.FindAll(TreeScope.Children,
                        //  new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
                        //
                        //var allMDIClients =  FindByMultipleConditions(MainWndAutElem);

                        List<IntPtr> allChildWindows = new WindowHandleInfo(processes[0].MainWindowHandle).GetAllChildHandles();
                        AutomationElement ael = AutomationElement.FromHandle(allChildWindows[0]);
                        Condition cond = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane);//.Window);
                        AutomationElementCollection collection = ael.FindAll(TreeScope.Children, cond);

                        AutomationElementCollection MainWndElementCollection = collection;// MainWndAutElem.FindAll(TreeScope.Children, MainWndConditions);

                        //MainWndElementCollection = allMDIClients;
                        if (MainWndElementCollection.Count > 0)
                        {
                            AutomationElement MDIClientAutElem = MainWndElementCollection[0];
                            if (MDIClientAutElem != null)
                            {

                                Condition MDIClientConditions = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                                //Возвращает все объекты AutomationElement, которые удовлетворяют заданному условию.
                                // Если совпадений нет, возвращается пустая коллекция.
                                AutomationElementCollection MDIClientElementCollection = MDIClientAutElem.FindAll(TreeScope.Children, MDIClientConditions);
                                if (MDIClientElementCollection.Count > 0)
                                {
                                    int mdiWidth = this.drawAreaRectangle.Right - this.drawAreaRectangle.Left;
                                    int mdiHeight = this.drawAreaRectangle.Bottom - this.drawAreaRectangle.Top;

                                    int activeViewX = this.drawAreaRectangle.Left;
                                    int activeViewY = this.drawAreaRectangle.Top;
                                    int activeViewWidth = mdiWidth;
                                    int activeViewHeight = mdiHeight;

                                    int otherViewsX = 0;
                                    int otherViewsWidth = 0;
                                    int otherViewsHeight = 0;

                                    if (MDIClientElementCollection.Count > 1)
                                    {
                                        activeViewX = this.drawAreaRectangle.Left;
                                        activeViewY = this.drawAreaRectangle.Top;
                                        activeViewWidth = (int)Math.Floor((double)(0.01 * tyleStep * mdiWidth));
                                        activeViewHeight = mdiHeight;

                                        otherViewsX = activeViewX + activeViewWidth + 1;
                                        otherViewsWidth = mdiWidth - activeViewWidth;
                                        otherViewsHeight = (int)Math.Floor((double)(mdiHeight / (MDIClientElementCollection.Count - 1)));
                                    }

                                    int activeViewIndex = getActiveViewIndex(MDIClientElementCollection);

                                    int otherViewNum = 0;
                                    for (int v = 0; v < MDIClientElementCollection.Count; v++)
                                    {
                                        AutomationElement ViewWndAutElem = MDIClientElementCollection[v];
                                        if (ViewWndAutElem != null)
                                        {
                                            try
                                            {
                                                WindowPattern ViewWindowPattern = ViewWndAutElem.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                                                if (ViewWindowPattern != null)
                                                {
                                                    ViewWindowPattern.SetWindowVisualState(WindowVisualState.Normal);

                                                    TransformPattern ViewTransformPattern = ViewWndAutElem.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
                                                    if (ViewTransformPattern != null)
                                                    {
                                                        if (v == activeViewIndex)
                                                        {
                                                            ViewTransformPattern.Move(activeViewX, activeViewY);
                                                            ViewTransformPattern.Resize(activeViewWidth, activeViewHeight);
                                                        }
                                                        else
                                                        {
                                                            ViewTransformPattern.Move(otherViewsX, (activeViewY + otherViewNum * otherViewsHeight));
                                                            ViewTransformPattern.Resize(otherViewsWidth, otherViewsHeight);
                                                            otherViewNum++;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception exc)
                                            {
                                            }
                                        }
                                    }


                                    foreach (UIView curUIView in this.uidoc.GetOpenUIViews())
                                    {
                                        curUIView.ZoomToFit();
                                    }

                                    this.tyleType = TileType.Left;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// setWindowsTileRight
        /// </summary>
        private void setWindowsTileRight()
        {
            Process[] processes = Process.GetProcessesByName("Revit");

            if (0 < processes.Length)
            {
                IntPtr mainWinHandle = processes[0].MainWindowHandle;
                mainWinHandle = uiapp.MainWindowHandle; //Autodesk.Revit.UI.UIApplication.MainWindowHandle;// processes[0].MainWindowHandle;
                if (mainWinHandle != null)
                {
                    AutomationElement MainWndAutElem = AutomationElement.FromHandle(mainWinHandle);
                    if (MainWndAutElem != null)
                    {
                        Condition MainWndConditions = new AndCondition(
                          new PropertyCondition(AutomationElement.ClassNameProperty, "MDIClient"),
                          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
                          );

                        AutomationElementCollection MainWndElementCollection = MainWndAutElem.FindAll(TreeScope.Children, MainWndConditions);
                        if (MainWndElementCollection.Count > 0)
                        {
                            AutomationElement MDIClientAutElem = MainWndElementCollection[0];
                            if (MDIClientAutElem != null)
                            {

                                Condition MDIClientConditions = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);

                                AutomationElementCollection MDIClientElementCollection = MDIClientAutElem.FindAll(TreeScope.Children, MDIClientConditions);
                                if (MDIClientElementCollection.Count > 0)
                                {
                                    int mdiWidth = this.drawAreaRectangle.Right - this.drawAreaRectangle.Left;
                                    int mdiHeight = this.drawAreaRectangle.Bottom - this.drawAreaRectangle.Top;

                                    int activeViewX = this.drawAreaRectangle.Left;
                                    int activeViewY = this.drawAreaRectangle.Top;
                                    int activeViewWidth = mdiWidth;
                                    int activeViewHeight = mdiHeight;

                                    int otherViewsX = 0;
                                    int otherViewsWidth = 0;
                                    int otherViewsHeight = 0;

                                    if (MDIClientElementCollection.Count > 1)
                                    {
                                        activeViewWidth = (int)Math.Floor((double)(0.01 * tyleStep * mdiWidth));
                                        activeViewHeight = mdiHeight;

                                        otherViewsWidth = mdiWidth - activeViewWidth;
                                        otherViewsHeight = (int)Math.Floor((double)(mdiHeight / (MDIClientElementCollection.Count - 1)));

                                        otherViewsX = this.drawAreaRectangle.Left;
                                        activeViewX = otherViewsX + otherViewsWidth + 1;
                                        activeViewY = this.drawAreaRectangle.Top;
                                    }

                                    int activeViewIndex = getActiveViewIndex(MDIClientElementCollection);

                                    int otherViewNum = 0;
                                    for (int v = 0; v < MDIClientElementCollection.Count; v++)
                                    {
                                        AutomationElement ViewWndAutElem = MDIClientElementCollection[v];
                                        if (ViewWndAutElem != null)
                                        {
                                            try
                                            {
                                                WindowPattern ViewWindowPattern = ViewWndAutElem.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                                                if (ViewWindowPattern != null)
                                                {
                                                    ViewWindowPattern.SetWindowVisualState(WindowVisualState.Normal);

                                                    TransformPattern ViewTransformPattern = ViewWndAutElem.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
                                                    if (ViewTransformPattern != null)
                                                    {
                                                        if (v == activeViewIndex)
                                                        {
                                                            ViewTransformPattern.Move(activeViewX, activeViewY);
                                                            ViewTransformPattern.Resize(activeViewWidth, activeViewHeight);
                                                        }
                                                        else
                                                        {
                                                            ViewTransformPattern.Move(otherViewsX, (activeViewY + otherViewNum * otherViewsHeight));
                                                            ViewTransformPattern.Resize(otherViewsWidth, otherViewsHeight);
                                                            otherViewNum++;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception exc)
                                            {
                                            }
                                        }
                                    }


                                    foreach (UIView curUIView in this.uidoc.GetOpenUIViews())
                                    {
                                        curUIView.ZoomToFit();
                                    }
                                    this.tyleType = TileType.Right;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void setWindowsTileTop()
        {
            Process[] processes = Process.GetProcessesByName("Revit");

            if (0 < processes.Length)
            {
                IntPtr mainWinHandle = processes[0].MainWindowHandle;
                if (mainWinHandle != null)
                {
                    AutomationElement MainWndAutElem = AutomationElement.FromHandle(mainWinHandle);
                    if (MainWndAutElem != null)
                    {
                        Condition MainWndConditions = new AndCondition(
                          new PropertyCondition(AutomationElement.ClassNameProperty, "MDIClient"),
                          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
                          );

                        AutomationElementCollection MainWndElementCollection = MainWndAutElem.FindAll(TreeScope.Children, MainWndConditions);
                        if (MainWndElementCollection.Count > 0)
                        {
                            AutomationElement MDIClientAutElem = MainWndElementCollection[0];
                            if (MDIClientAutElem != null)
                            {

                                Condition MDIClientConditions = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);

                                AutomationElementCollection MDIClientElementCollection = MDIClientAutElem.FindAll(TreeScope.Children, MDIClientConditions);
                                if (MDIClientElementCollection.Count > 0)
                                {
                                    int mdiWidth = this.drawAreaRectangle.Right - this.drawAreaRectangle.Left;
                                    int mdiHeight = this.drawAreaRectangle.Bottom - this.drawAreaRectangle.Top;

                                    int activeViewX = this.drawAreaRectangle.Left;
                                    int activeViewY = this.drawAreaRectangle.Top;
                                    int activeViewWidth = mdiWidth;
                                    int activeViewHeight = mdiHeight;

                                    int otherViewsY = 0;
                                    int otherViewsWidth = 0;
                                    int otherViewsHeight = 0;

                                    if (MDIClientElementCollection.Count > 1)
                                    {
                                        activeViewX = this.drawAreaRectangle.Left;
                                        activeViewY = this.drawAreaRectangle.Top;
                                        activeViewWidth = mdiWidth;
                                        activeViewHeight = (int)Math.Floor((double)(0.01 * tyleStep * mdiHeight));

                                        otherViewsY = activeViewY + activeViewHeight + 1;
                                        otherViewsWidth = (int)Math.Floor((double)(mdiWidth / (MDIClientElementCollection.Count - 1)));
                                        otherViewsHeight = mdiHeight - activeViewHeight;
                                    }

                                    int activeViewIndex = getActiveViewIndex(MDIClientElementCollection);

                                    int otherViewNum = 0;
                                    for (int v = 0; v < MDIClientElementCollection.Count; v++)
                                    {
                                        AutomationElement ViewWndAutElem = MDIClientElementCollection[v];
                                        if (ViewWndAutElem != null)
                                        {
                                            try
                                            {
                                                WindowPattern ViewWindowPattern = ViewWndAutElem.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                                                if (ViewWindowPattern != null)
                                                {
                                                    ViewWindowPattern.SetWindowVisualState(WindowVisualState.Normal);

                                                    TransformPattern ViewTransformPattern = ViewWndAutElem.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
                                                    if (ViewTransformPattern != null)
                                                    {
                                                        if (v == activeViewIndex)
                                                        {
                                                            ViewTransformPattern.Move(activeViewX, activeViewY);
                                                            ViewTransformPattern.Resize(activeViewWidth, activeViewHeight);
                                                        }
                                                        else
                                                        {
                                                            ViewTransformPattern.Move((activeViewX + otherViewNum * otherViewsWidth), otherViewsY);
                                                            ViewTransformPattern.Resize(otherViewsWidth, otherViewsHeight);
                                                            otherViewNum++;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception exc)
                                            {
                                            }
                                        }
                                    }


                                    foreach (UIView curUIView in this.uidoc.GetOpenUIViews())
                                    {
                                        curUIView.ZoomToFit();
                                    }
                                    this.tyleType = TileType.Top;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void setWindowsTileBottom()
        {
            Process[] processes = Process.GetProcessesByName("Revit");

            if (0 < processes.Length)
            {
                IntPtr mainWinHandle = processes[0].MainWindowHandle;
                if (mainWinHandle != null)
                {
                    AutomationElement MainWndAutElem = AutomationElement.FromHandle(mainWinHandle);
                    if (MainWndAutElem != null)
                    {
                        Condition MainWndConditions = new AndCondition(
                          new PropertyCondition(AutomationElement.ClassNameProperty, "MDIClient"),
                          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
                          );

                        AutomationElementCollection MainWndElementCollection = MainWndAutElem.FindAll(TreeScope.Children, MainWndConditions);
                        if (MainWndElementCollection.Count > 0)
                        {
                            AutomationElement MDIClientAutElem = MainWndElementCollection[0];
                            if (MDIClientAutElem != null)
                            {
                                Condition MDIClientConditions = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);

                                AutomationElementCollection MDIClientElementCollection = MDIClientAutElem.FindAll(TreeScope.Children, MDIClientConditions);
                                if (MDIClientElementCollection.Count > 0)
                                {
                                    int mdiWidth = this.drawAreaRectangle.Right - this.drawAreaRectangle.Left;
                                    int mdiHeight = this.drawAreaRectangle.Bottom - this.drawAreaRectangle.Top;

                                    int activeViewX = this.drawAreaRectangle.Left;
                                    int activeViewY = this.drawAreaRectangle.Top;
                                    int activeViewWidth = mdiWidth;
                                    int activeViewHeight = mdiHeight;

                                    int otherViewsY = 0;
                                    int otherViewsWidth = 0;
                                    int otherViewsHeight = 0;

                                    if (MDIClientElementCollection.Count > 1)
                                    {
                                        activeViewWidth = mdiWidth;
                                        activeViewHeight = (int)Math.Floor((double)(0.01 * tyleStep * mdiHeight));

                                        otherViewsWidth = (int)Math.Floor((double)(mdiWidth / (MDIClientElementCollection.Count - 1)));
                                        otherViewsHeight = mdiHeight - activeViewHeight;

                                        otherViewsY = this.drawAreaRectangle.Top;
                                        activeViewX = this.drawAreaRectangle.Left;
                                        activeViewY = otherViewsY + otherViewsHeight + 1;
                                    }

                                    int activeViewIndex = getActiveViewIndex(MDIClientElementCollection);

                                    int otherViewNum = 0;
                                    for (int v = 0; v < MDIClientElementCollection.Count; v++)
                                    {
                                        AutomationElement ViewWndAutElem = MDIClientElementCollection[v];
                                        if (ViewWndAutElem != null)
                                        {
                                            try
                                            {
                                                WindowPattern ViewWindowPattern = ViewWndAutElem.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                                                if (ViewWindowPattern != null)
                                                {///test why pattern is not matched?
                                                    ViewWindowPattern.SetWindowVisualState(WindowVisualState.Normal);

                                                    TransformPattern ViewTransformPattern = ViewWndAutElem.GetCurrentPattern(TransformPattern.Pattern) as TransformPattern;
                                                    if (ViewTransformPattern != null)
                                                    {
                                                        if (v == activeViewIndex)
                                                        {
                                                            ViewTransformPattern.Move(activeViewX, activeViewY);
                                                            ViewTransformPattern.Resize(activeViewWidth, activeViewHeight);
                                                        }
                                                        else
                                                        {
                                                            ViewTransformPattern.Move((activeViewX + otherViewNum * otherViewsWidth), otherViewsY);
                                                            ViewTransformPattern.Resize(otherViewsWidth, otherViewsHeight);
                                                            otherViewNum++;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception exc)
                                            {
                                            }
                                        }
                                    }

                                    foreach (UIView curUIView in this.uidoc.GetOpenUIViews())
                                    {
                                        curUIView.ZoomToFit();
                                    }

                                    this.tyleType = TileType.Bottom;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Rectangle GetActiveViewRectangel()
        {
            Autodesk.Revit.DB.View activeView = this.uidoc.ActiveView;

            foreach (UIView curUIView in this.uidoc.GetOpenUIViews())
            {
                if (curUIView.ViewId == activeView.Id)
                    return curUIView.GetWindowRectangle();
            }

            throw new Exception("No Active View");
        }

        private int getActiveViewIndex(AutomationElementCollection MDIClientElementCollection)
        {
            Rectangle viewRect = GetActiveViewRectangel();

            int viewIndex = -1;
            double rectSizeDiff = 0;

            System.Windows.Rect[] boundingRectangles = new System.Windows.Rect[MDIClientElementCollection.Count];
            for (int v = 0; v < MDIClientElementCollection.Count; v++)
            {
                AutomationElement ViewWndAutElem = MDIClientElementCollection[v];
                if (ViewWndAutElem != null)
                {
                    try
                    {
                        AutomationProperty[] curProperties = ViewWndAutElem.GetSupportedProperties();
                        foreach (AutomationProperty curProperty in curProperties)
                        {
                            if (curProperty.ProgrammaticName == "AutomationElementIdentifiers.BoundingRectangleProperty")
                            {
                                System.Object curPropertyValue = ViewWndAutElem.GetCurrentPropertyValue(curProperty);
                                if (curPropertyValue is System.Windows.Rect)
                                {
                                    System.Windows.Rect curRect = (System.Windows.Rect)curPropertyValue;
                                    if (curRect != null)
                                    {
                                        if (curRect.Left < viewRect.Left && curRect.Right > viewRect.Right && curRect.Top < viewRect.Top && curRect.Bottom > viewRect.Bottom)
                                        {
                                            if (viewIndex == -1)
                                            {
                                                rectSizeDiff = (viewRect.Left - curRect.Left) + (curRect.Right - viewRect.Right) + (viewRect.Top - curRect.Top) + (curRect.Bottom - viewRect.Bottom);
                                                viewIndex = v;
                                            }
                                            else
                                            {
                                                double diff = (viewRect.Left - curRect.Left) + (curRect.Right - viewRect.Right) + (viewRect.Top - curRect.Top) + (curRect.Bottom - viewRect.Bottom);
                                                if (diff < rectSizeDiff)
                                                {
                                                    rectSizeDiff = diff;
                                                    viewIndex = v;
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return viewIndex;
        }

        private void setWindowsTileBigger()
        {
            if (this.tyleStep < 80)
            {
                this.tyleStep += 5;

                switch (this.tyleType)
                {
                    case TileType.Left:
                        setWindowsTileLeft();
                        break;
                    case TileType.Right:
                        setWindowsTileRight();
                        break;
                    case TileType.Top:
                        setWindowsTileTop();
                        break;
                    case TileType.Bottom:
                        setWindowsTileBottom();
                        break;
                    default:
                        break;
                }
            }
        }

        private void setWindowsTileSmaller()
        {
            if (this.tyleStep > 20)
            {
                this.tyleStep -= 5;

                switch (this.tyleType)
                {
                    case TileType.Left:
                        setWindowsTileLeft();
                        break;
                    case TileType.Right:
                        setWindowsTileRight();
                        break;
                    case TileType.Top:
                        setWindowsTileTop();
                        break;
                    case TileType.Bottom:
                        setWindowsTileBottom();
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// [Load]
        /// </summary>
        private void setWindowsTileLoad()
        {
            if (File.Exists(KbBimstoreApp.TileSettingsFilePath))
            {
                try
                {
                    XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));

                    if (File.Exists(KbBimstoreApp.TileSettingsFilePath))
                    {
                        TextReader reader = new StreamReader(KbBimstoreApp.TileSettingsFilePath);
                        DataSet myDataSet = (DataSet)xmlSer.Deserialize(reader);
                        reader.Close();

                        if (!myDataSet.Tables.Contains("Settings"))
                        {
                            return;
                        }

                        DataTable settingsTable = myDataSet.Tables["Settings"];

                        if (settingsTable.Columns.Count < 2)
                        {
                            return;
                        }

                        if (settingsTable.Rows.Count < 2)
                        {
                            return;
                        }

                        DataRow dataRow1 = settingsTable.Rows[0];
                        if (dataRow1[0].ToString() == "TyleType")
                        {
                            try
                            {
                                int tt = Convert.ToInt32(dataRow1[1].ToString());

                                if (tt == -1)
                                    this.tyleType = TileType.Center;
                                else
                                    this.tyleType = (TileType)tt;
                            }
                            catch (Exception ex)
                            {
                                this.tyleType = TileType.Center;
                            }
                        }

                        DataRow dataRow2 = settingsTable.Rows[1];
                        if (dataRow2[0].ToString() == "TyleStep")
                        {
                            try
                            {
                                this.tyleStep = Convert.ToInt32(dataRow2[1].ToString());
                            }
                            catch (Exception ex)
                            {
                                this.tyleStep = 70;
                            }
                        }

                        switch (this.tyleType)
                        {
                            case TileType.Left:
                                setWindowsTileLeft();
                                break;
                            case TileType.Right:
                                setWindowsTileRight();
                                break;
                            case TileType.Top:
                                setWindowsTileTop();
                                break;
                            case TileType.Bottom:
                                setWindowsTileBottom();
                                break;
                            default:
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                TaskDialog.Show("Error", "There are no saved settings");
            }
        }
        /// <summary>
        /// [Save]
        /// </summary>
        private void setWindowsTileSave()
        {

            try
            {
                DataSet myDataSet = new DataSet("DataSet");

                DataTable settingsTable = new DataTable("Settings");
                DataColumn settingsColumnName = new DataColumn("Name", System.Type.GetType("System.String"));
                DataColumn settingsColumnValue = new DataColumn("Value", System.Type.GetType("System.Int32"));
                settingsTable.Columns.Add(settingsColumnName);
                settingsTable.Columns.Add(settingsColumnValue);
                myDataSet.Tables.Add(settingsTable);

                DataRow dataRow1 = settingsTable.NewRow();
                dataRow1[0] = "TyleType";
                dataRow1[1] = (int)this.tyleType;
                settingsTable.Rows.Add(dataRow1);

                DataRow dataRow2 = settingsTable.NewRow();
                dataRow2[0] = "TyleStep";
                dataRow2[1] = this.tyleStep;
                settingsTable.Rows.Add(dataRow2);

                XmlSerializer xmlSer = new XmlSerializer(typeof(DataSet));
                TextWriter writer = new StreamWriter(KbBimstoreApp.TileSettingsFilePath);
                xmlSer.Serialize(writer, myDataSet);
                writer.Close();

                TaskDialog.Show("Info", "Window Tile settings were saved");


            }
            catch (Exception ex)
            {
                TaskDialog.Show("Exception", ex.Message);
            }


        }
    }
    //
    public class WindowHandleInfo
    {
        private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);

        private IntPtr _MainHandle;

        public WindowHandleInfo(IntPtr handle)
        {
            this._MainHandle = handle;
        }

        public List<IntPtr> GetAllChildHandles()
        {
            List<IntPtr> childHandles = new List<IntPtr>();

            GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
            IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(this._MainHandle, childProc, pointerChildHandlesList);
            }
            finally
            {
                gcChildhandlesList.Free();
            }

            return childHandles;
        }

        private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
        {
            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
            {
                return false;
            }

            List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
            childHandles.Add(hWnd);

            return true;
        }
    }
}
