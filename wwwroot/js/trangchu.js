$(document).ready(function () {
    const audioPlayer = document.getElementById('audioPlayer');
    const musicPlayer = document.getElementById('musicPlayer');
    const playPauseBtn = document.getElementById('playPauseBtn');
    const playPauseIcon = document.getElementById('playPauseIcon');
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');
    const volumeControl = document.getElementById('volumeControl');
    const songProgress = document.getElementById('songProgress');
    const currentTime = document.getElementById('currentTime');
    const totalTime = document.getElementById('totalTime');
    const currentSongTitle = document.getElementById('currentSongTitle');
    const currentSongArtist = document.getElementById('currentSongArtist');
    const currentSongImage = document.getElementById('currentSongImage');
    const closePlayerBtn = document.getElementById('closePlayerBtn');
    
    // Khởi tạo danh sách phát
    let playlist = [];
    let currentSongIndex = 0;
    
    // Thu thập thông tin bài hát từ các button phát nhạc
    $('.play-button').each(function() {
        playlist.push({
            url: $(this).data('song-url'),
            title: $(this).data('song-title'),
            artist: 'Unknown', // Có thể thêm thông tin nghệ sĩ nếu có
        });
    });
    
    // Xử lý sự kiện click vào nút phát nhạc
    $('.play-button').on('click', function() {
        const songUrl = $(this).data('song-url');
        const songTitle = $(this).data('song-title');
        
        // Tìm index của bài hát trong playlist
        currentSongIndex = playlist.findIndex(song => song.url === songUrl);
        
        // Phát nhạc
        playSong(songUrl, songTitle, 'Unknown');
        
        // Hiển thị thanh phát nhạc
        $(musicPlayer).addClass('show');
    });
    
    // Hàm phát nhạc
    function playSong(url, title, artist) {
        // Cập nhật thông tin bài hát
        currentSongTitle.textContent = title;
        currentSongArtist.textContent = artist;
        
        // Cập nhật đường dẫn audio
        audioPlayer.src = url;
        audioPlayer.load();
        
        // Phát nhạc
        audioPlayer.play()
            .then(() => {
                playPauseIcon.classList.remove('fa-play');
                playPauseIcon.classList.add('fa-pause');
            })
            .catch(error => {
                console.error('Error playing audio:', error);
            });
    }
    
    // Xử lý sự kiện play/pause
    playPauseBtn.addEventListener('click', function() {
        if (audioPlayer.paused) {
            audioPlayer.play();
            playPauseIcon.classList.remove('fa-play');
            playPauseIcon.classList.add('fa-pause');
        } else {
            audioPlayer.pause();
            playPauseIcon.classList.remove('fa-pause');
            playPauseIcon.classList.add('fa-play');
        }
    });
    
    // Xử lý sự kiện prev
    prevBtn.addEventListener('click', function() {
        if (playlist.length === 0) return;
        
        currentSongIndex = (currentSongIndex - 1 + playlist.length) % playlist.length;
        const song = playlist[currentSongIndex];
        playSong(song.url, song.title, song.artist);
    });
    
    // Xử lý sự kiện next
    nextBtn.addEventListener('click', function() {
        if (playlist.length === 0) return;
        
        currentSongIndex = (currentSongIndex + 1) % playlist.length;
        const song = playlist[currentSongIndex];
        playSong(song.url, song.title, song.artist);
    });
    
    // Xử lý sự kiện volume change
    volumeControl.addEventListener('input', function() {
        audioPlayer.volume = this.value;
    });
    
    // Cập nhật thanh progress và thời gian
    audioPlayer.addEventListener('timeupdate', function() {
        const progress = (audioPlayer.currentTime / audioPlayer.duration) * 100;
        songProgress.style.width = `${progress}%`;
        
        // Cập nhật thời gian hiện tại
        const curMinutes = Math.floor(audioPlayer.currentTime / 60);
        const curSeconds = Math.floor(audioPlayer.currentTime % 60);
        currentTime.textContent = `${curMinutes}:${curSeconds < 10 ? '0' + curSeconds : curSeconds}`;
        
        // Cập nhật tổng thời gian nếu có
        if (!isNaN(audioPlayer.duration)) {
            const totalMinutes = Math.floor(audioPlayer.duration / 60);
            const totalSeconds = Math.floor(audioPlayer.duration % 60);
            totalTime.textContent = `${totalMinutes}:${totalSeconds < 10 ? '0' + totalSeconds : totalSeconds}`;
        }
    });
    
    // Xử lý sự kiện kết thúc bài hát
    audioPlayer.addEventListener('ended', function() {
        // Chuyển sang bài tiếp theo
        nextBtn.click();
    });
    
    // Xử lý sự kiện đóng player
    closePlayerBtn.addEventListener('click', function() {
        $(musicPlayer).removeClass('show');
        audioPlayer.pause();
        playPauseIcon.classList.remove('fa-pause');
        playPauseIcon.classList.add('fa-play');
    });


    // Tìm kiếm bài hát
    const searchInput = $("#searchInput");
    const searchResults = $("#searchResults");
    const searchResultsList = $(".search-results-list");
    const searchLoading = $(".search-loading");
    const searchNoResults = $(".search-no-results");
    const searchViewAll = $(".search-view-all");
    
    let searchTimeout;
    let currentSearchTerm = '';
    
    searchInput.on("input", function() {
        const searchTerm = $(this).val().trim();
        currentSearchTerm = searchTerm;
        clearTimeout(searchTimeout);
        
        if (searchTerm === '') {
            searchResults.hide();
            return;
        }

        searchTimeout = setTimeout(function() {
            searchResults.show();
            searchResultsList.empty();
            searchLoading.removeClass('d-none');
            searchNoResults.addClass('d-none');
            searchViewAll.addClass('d-none');
            
            $.ajax({
                url: 'search',
                type: 'GET',
                data: { q: searchTerm, limit: 5 },
                success: function(response) {
                    if (searchTerm !== currentSearchTerm) return;
                    
                    searchLoading.addClass('d-none');
                    
                    if (response.results && response.results.length > 0) {
                        searchResultsList.empty();
                        response.results.forEach(function(item) {
                            const resultItem = `
                                <a href="/MusicInfo?id=${item.id}" class="search-result-item">
                                    <img src="${item.imageUrl || 'https://via.placeholder.com/50'}" alt="${item.title}" class="search-result-image">
                                    <div class="search-result-info">
                                        <div class="search-result-title">${item.title}</div>
                                        <div class="search-result-subtitle">${item.artist || 'Unknown artist'}</div>
                                    </div>
                                    <div class="search-result-play">
                                        <button class="btn btn-sm btn-outline-primary rounded-circle play-search-result" 
                                            data-song-url="${item.fileUrl}" 
                                            data-song-title="${item.title}"
                                            data-song-artist="${item.artist || 'Unknown artist'}">
                                            <i class="fa-solid fa-play"></i>
                                        </button>
                                    </div>
                                </a>
                            `;
                            searchResultsList.append(resultItem);
                        });
                        if (response.totalCount > response.results.length) {
                            searchViewAll.removeClass('d-none');
                            $('.view-all-results').attr('href', `/Home/SearchResults?q=${encodeURIComponent(searchTerm)}`);
                        }
                    } else {
                        searchNoResults.removeClass('d-none');
                    }
                },
                error: function() {
                    searchLoading.addClass('d-none');
                    searchResultsList.html('<div class="text-danger p-3">Có lỗi xảy ra khi tìm kiếm</div>');
                }
            });
        }, 300);
    });
    
    
    $(document).on('click', function(e) {
        if (!$(e.target).closest('.search_bar').length) {
            searchResults.hide();
        }
    });
    
    searchInput.focus(function() {
        if ($(this).val().trim() !== '') {
            searchResults.show();
        }
    });
    
    $(document).on('click', '.play-search-result', function(e) {
        e.preventDefault();
        e.stopPropagation();
        
        const songUrl = $(this).data('song-url');
        const songTitle = $(this).data('song-title');
        const songArtist = $(this).data('song-artist');
        
        playSong(songUrl, songTitle, songArtist);
        $(musicPlayer).addClass('show');
    });
});

