## Features

Just a quick note on Features.

All features in the features folder should use FeatureRoot as the root element. Feature Root contains the
common 'Save' and 'Select Feature' buttons that all features will use, as well as expose those events.

Implement whatever save functionality is required by each feature by binding to FeatureRoot's
'selected' event. The parent modal also subscribes to this event (and cancel) which works nicely to close the modal.

Consistency is required here as features are loaded dynamically and as far as I can tell there isn't
a way to dynamically bind event handlers to the built-in component Component.
