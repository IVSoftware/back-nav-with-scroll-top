It's a bit tricky in terms of timing, because the layout needs to be fully calculated before you can do something like `scrollView.ScrollToAsync(0, 0, false)`. By triggering on the `NavigatedTo` event and then using scheduling the action at the end of the existing message queue, you should see reliable timing. 

