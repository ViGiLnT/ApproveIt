;(function ( $ ) {
  /**
   * @class Represents a single reference.
   */
  var CompareData =  function(referenceData, options){
    /* global DiffHandler */
    this.dmp =  new DiffHandler();
    this.dmp.Diff_Timeout = options ? options.timeout || 0 : 0;

    for(var key in referenceData){
      this[key] = referenceData[key];
    }
  };

  CompareData.prototype = {
    /**
     * Wordbased diff with
     * @return {Array}  EXPLAIN_PROTOCOL
     */
    getDiff :function(){
      //lazy loading diffs
      if(!this.diff){
        var left = this.left;
        var right = this.right;
        this.diff = this.dmp.diff_wordbased(left, right,false);
        //dmp.diff_cleanupSemantic(this.diff);
      }
      return this.diff;
    },
    /**
     * Diff as aligned html
     * @param  {Number} maxchars Max line length
     * @return {String}
     */
    getHtmlTexts : function(maxchars){
      var diff = this.getDiff();
      return this.dmp.alligned_texts(diff, maxchars);
    },
    /**
     * Diff as strict aligned html
     * @param  {Number} maxchars line length
     * @return {String}
     */
    getHtmlTextStrict : function(maxchars){
      var diff = this.getDiff();
      return this.dmp.alligned_texts_strict(diff, maxchars);

    }
  };

  /*
   * Plugin functions
   */
	$.fn.picadiff = function(options){
		//check if WebFont API is present
		if(window['WebFont'] !== undefined) {
			/* global WebFont */
			WebFont.load({google: {families: ['Source Code Pro']}});
		}
		return this.each(function(){
			var $this = $(this).addClass('picadiff');

      var settings = {
        leftContainer     : '.left',
        rightContainer    : '.right',
        titleContainer    : '.picadiff-title',
        contentContainer  : '.picadiff-content',
        lineLength        : 40,
        wrap              : false


      };
      // If options exist, lets merge them
      // with our default settings
      if ( options ) {
        $.extend( settings, options );
      }

      var referenceData = {
        leftTitle : settings.leftTitle  || $this.find(settings.titleContainer+' '+settings.leftContainer).text(),
        left    : settings.leftContent  || $this.find(settings.contentContainer+' '+settings.leftContainer).text(),
        rightTitle  : settings.rightTitle || $this.find(settings.titleContainer+' '+settings.rightContainer).text(),
        right   : settings.rightContent || $this.find(settings.contentContainer+' '+settings.rightContainer).text()
      };

      var compareData = new CompareData(referenceData, settings);
  
      var lineLength = settings.lineLength;
      var html_texts = settings.wrap ? compareData.getHtmlTextStrict(lineLength) :
        compareData.getHtmlTexts(lineLength);

      var left = html_texts.source_html;
      var right = html_texts.diss_html;

      $(settings.contentContainer+" "+settings.leftContainer).html(left);
      $(settings.contentContainer+" "+settings.rightContainer).html(right);
      $(settings.titleContainer+" "+settings.leftContainer).html(compareData.leftTitle);
      $(settings.titleContainer+" "+settings.rightContainer).html(compareData.rightTitle);
    });
  };
})( jQuery );

